using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace 实验4.Tests
{
    [TestClass]
    public class LiveStreamManagerTests
    {
        /// <summary>
        /// 测试1：并发数限制 —— 同时启动不超过指定上限
        /// </summary>
        [TestMethod]
        public void TryStart_超过并发上限_应返回false()
        {
            // Arrange: 最大并发数3
            using (var manager = new LiveStreamManager(3))
            {
                // Act: 启动前3路
                bool r1 = manager.TryStart(1);
                bool r2 = manager.TryStart(2);
                bool r3 = manager.TryStart(3);
                // 第4路应被拒绝
                bool r4 = manager.TryStart(4);

                // Assert
                Assert.IsTrue(r1, "第1路应成功");
                Assert.IsTrue(r2, "第2路应成功");
                Assert.IsTrue(r3, "第3路应成功");
                Assert.IsFalse(r4, "第4路应被拒绝（已达上限）");
                Assert.AreEqual(3, manager.ActiveCount);
            }
        }

        /// <summary>
        /// 测试2：停止后释放槽位 —— 可启动新流
        /// </summary>
        [TestMethod]
        public void Stop_释放槽位后_可重新启动()
        {
            // Arrange: 占满3个槽位
            using (var manager = new LiveStreamManager(3))
            {
                manager.TryStart(1);
                manager.TryStart(2);
                manager.TryStart(3);
                Assert.AreEqual(3, manager.ActiveCount);

                // Act: 停止第2路
                bool stopped = manager.Stop(2);

                // Assert: 槽位释放，第4路可启动
                Assert.IsTrue(stopped);
                Assert.AreEqual(2, manager.ActiveCount);
                Assert.IsTrue(manager.TryStart(4), "停止后释放的槽位应允许新流启动");
                Assert.AreEqual(3, manager.ActiveCount);
            }
        }

        /// <summary>
        /// 测试3：重复启动防护 —— 同一ID不能重复启动
        /// </summary>
        [TestMethod]
        public void TryStart_重复启动同一流_应返回false()
        {
            // Arrange
            using (var manager = new LiveStreamManager(3))
            {
                // Act: 第1路启动两次
                bool first = manager.TryStart(1);
                bool second = manager.TryStart(1);

                // Assert
                Assert.IsTrue(first, "首次启动应成功");
                Assert.IsFalse(second, "重复启动应被拒绝");
                Assert.AreEqual(1, manager.ActiveCount, "活跃数应仍为1，未重复计数");
            }
        }

        /// <summary>
        /// 测试4：全部停止后状态归零
        /// </summary>
        [TestMethod]
        public void StopAll_应停止所有流_活跃数归零()
        {
            // Arrange
            using (var manager = new LiveStreamManager(3))
            {
                manager.TryStart(1);
                manager.TryStart(2);
                manager.TryStart(3);

                // Act
                int stopped = manager.StopAll();

                // Assert
                Assert.AreEqual(3, stopped, "应停止3路");
                Assert.AreEqual(0, manager.ActiveCount, "活跃数应归零");
                Assert.AreEqual(3, manager.AvailableSlots, "所有槽位恢复可用");
            }
        }
    }
}
