using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace 实验4
{
    /// <summary>
    /// 直播流并发管理器 —— 控制同时直播的数量上限。
    /// 将并发控制逻辑从 UI 层解耦，使其可独立测试。
    /// </summary>
    public class LiveStreamManager : IDisposable
    {
        private readonly int _maxConcurrency;
        private int _activeCount;
        private readonly Dictionary<int, bool> _streams;
        private readonly object _lock = new object();

        /// <summary>最大并发数</summary>
        public int MaxConcurrency => _maxConcurrency;

        /// <summary>当前活跃的直播流数量</summary>
        public int ActiveCount
        {
            get { int v = Volatile.Read(ref _activeCount); return v; }
        }

        /// <summary>可用槽位数</summary>
        public int AvailableSlots => _maxConcurrency - ActiveCount;

        public LiveStreamManager(int maxConcurrency)
        {
            if (maxConcurrency <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxConcurrency),
                    "最大并发数必须大于0");

            _maxConcurrency = maxConcurrency;
            _streams = new Dictionary<int, bool>();
        }

        /// <summary>
        /// 尝试启动一个直播流。
        /// </summary>
        /// <param name="streamId">直播流唯一ID</param>
        /// <returns>true=启动成功；false=槽位已满 或 该流已在运行</returns>
        public bool TryStart(int streamId)
        {
            lock (_lock)
            {
                // 已在运行 → 拒绝重复启动
                if (_streams.TryGetValue(streamId, out bool running) && running)
                    return false;

                // 槽位已满 → 拒绝
                if (ActiveCount >= _maxConcurrency)
                    return false;

                // 分配槽位
                _streams[streamId] = true;
                Interlocked.Increment(ref _activeCount);
                return true;
            }
        }

        /// <summary>
        /// 停止指定直播流，释放槽位。
        /// </summary>
        /// <param name="streamId">直播流ID</param>
        /// <returns>true=停止成功；false=该流未在运行</returns>
        public bool Stop(int streamId)
        {
            lock (_lock)
            {
                if (!_streams.TryGetValue(streamId, out bool running) || !running)
                    return false;

                _streams[streamId] = false;
                Interlocked.Decrement(ref _activeCount);
                return true;
            }
        }

        /// <summary>
        /// 停止所有正在运行的直播流。
        /// </summary>
        /// <returns>实际停止的流数量</returns>
        public int StopAll()
        {
            lock (_lock)
            {
                int stopped = 0;
                var keys = _streams.Keys.ToList();
                foreach (var id in keys)
                {
                    if (_streams[id])
                    {
                        _streams[id] = false;
                        stopped++;
                    }
                }
                _activeCount = 0;
                return stopped;
            }
        }

        /// <summary>
        /// 查询指定流是否正在运行。
        /// </summary>
        public bool IsRunning(int streamId)
        {
            lock (_lock)
            {
                return _streams.TryGetValue(streamId, out bool running) && running;
            }
        }

        public void Dispose()
        {
            // 无需清理非托管资源
        }
    }
}
