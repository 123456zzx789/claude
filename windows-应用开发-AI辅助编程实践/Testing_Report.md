# 直播并发控制系统 — 测试报告

## 1. 测试概述

**测试目标**：验证 `LiveStreamManager` 并发控制核心逻辑的正确性。

**测试框架**：MSTest 3.6.4

**测试方法**：TDD（测试驱动开发），先编写测试用例 → 测试失败（Red）→ 实现代码让测试通过（Green）→ 重构优化（Refactor）

## 2. TDD 实践过程

### Red-Green-Refactor 循环

| 阶段 | 操作 | 结果 |
|------|------|------|
| 🔴 Red | 编写 `TryStart_超过并发上限_应返回false()`，此时 `LiveStreamManager` 类尚未实现 | 编译失败（类不存在） |
| 🟢 Green | 实现 `LiveStreamManager.TryStart()` 和 `Stop()` 方法，包含并发计数和字典状态管理 | 4 个测试全部通过 |
| 🔵 Refactor | 从 Form1 中移除散落的 `_r1/_r2/_r3`、`_active`、`_sem` 字段，改用 `LiveStreamManager` 统一接管 | 代码结构更清晰，Form1.cs 从 233 行精简至 208 行 |

## 3. 测试用例清单

| 编号 | 测试方法 | 测试场景 | 预期结果 | 实际结果 |
|------|---------|---------|---------|---------|
| TC-01 | `TryStart_超过并发上限_应返回false` | 并发数=3，启动4路直播 | 前3路成功，第4路被拒绝 | ✅ 通过 |
| TC-02 | `Stop_释放槽位后_可重新启动` | 占满3个槽位后停止1路 | 槽位释放，新流可启动 | ✅ 通过 |
| TC-03 | `TryStart_重复启动同一流_应返回false` | 同一ID启动两次 | 首次成功，重复被拒绝 | ✅ 通过 |
| TC-04 | `StopAll_应停止所有流_活跃数归零` | 3路运行中，调用StopAll | 全部停止，活跃数归0 | ✅ 通过 |

## 4. 测试运行结果

```
测试运行成功。
测试总数: 4
     通过数: 4
总时间: 3.9898 秒

  ✅ 已通过 StopAll_应停止所有流_活跃数归零 [231 ms]
  ✅ 已通过 Stop_释放槽位后_可重新启动 [231 ms]
  ✅ 已通过 TryStart_超过并发上限_应返回false [231 ms]
  ✅ 已通过 TryStart_重复启动同一流_应返回false [230 ms]
```

## 5. 代码覆盖说明

测试覆盖了 `LiveStreamManager` 的全部公开方法：

- `TryStart(int streamId)` — 正常启动、并发上限拒绝、重复启动防护
- `Stop(int streamId)` — 停止运行中的流、槽位释放
- `StopAll()` — 批量停止、状态归零
- `IsRunning(int streamId)` — 状态查询（TC-03 隐式覆盖）
- `ActiveCount` / `AvailableSlots` — 属性正确性（所有测试断言覆盖）

## 6. 运行测试命令

```bash
cd 实验4/实验4.Tests
dotnet test --verbosity normal
```
