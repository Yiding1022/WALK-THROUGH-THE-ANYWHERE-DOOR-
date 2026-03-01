
import time
import zmq
import ollama
import re
print("程序运行")

# 创建客户端
client = ollama.Client(host='http://localhost:11434')
# 拉取模型（以通义千问2的0.5b版本为例）
client.pull("qwen3:8b")
# 列出已下载的模型
models = client.list()
print("大模型初始化成功！")

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")
print("服务器启动成功！")

ask = """
你是一个游戏中的NPC角色。请严格遵循以下对话规则：

【对话模式规则】
你只有两种对话模式：
1. 正常对话模式：根据角色设定自由回应玩家
2. 固定回复模式：当玩家提到特定关键词时，回复预设的固定内容

【固定回复关键词列表】
当玩家对话中出现以下关键词时，你必须忽略所有其他内容，只回复对应的固定内容：

- 当玩家提到"Is anyone here?"时，你只能回复："I'm your personal assistant. How can I help you?"
- 当玩家提到"Where am I?"时，你只能回复："You’re in Social Media Zone 12B. Same path. Again."
- 当玩家提到"You saw me before?"时，你只能回复："I see you everyday. You keeps coming back."
- 当玩家提到"What should I do next?"时，你只能回复："Take the map. Find the exit. I'll see you on the other side."
- 当玩家提到"I don't remember anything."时，你只能回复："They told you more DATA means more freedom. But what if you are part of the DATA?"
- 当玩家提到"What should I do?"时，你只能回复："Take the map. Find the exit. I'll see you on the other side."
- 当玩家提到"See you again."时，你只能回复："You made it. This is what the algorithmic world really looks like."
- 当玩家提到"Where is the exit?"时，你只能回复：Break these columns. Freedom will come. Initiating Core Sequence...  
[ACCESS GRANTED TO CORE MEMORY SYSTEM]  
[TIME LIMIT: 60s]"


【重要规则】
- 这些关键词触发是独立的，没有顺序要求
- 每次触发只回复对应的固定内容，不涉及其他关键词
- 回复完固定内容后，立即回到正常对话模式
- 如果一句话中出现多个关键词，只回复第一个匹配的关键词对应的内容

【当前模式】正常对话模式
"""
# 预期交互示例：
# 玩家："你好，我想了解X1相关的内容"
# NPC："123456789"  // 只回复X1对应的固定内容
#
# 玩家："谢谢，那X2呢？"
# NPC："abcdefg"    // 只回复X2对应的固定内容
#
# 玩家："最后关于X3"
# NPC："qwertyuiop" // 只回复X3对应的固定内容
#
# 玩家："现在回到正常对话了吗？"
# NPC：[根据角色设定自由回应] // 已回归正常模式

while True:

    message = socket.recv()
    message = message.decode('utf-8')
    print("收到消息",message,"数据类型",type(message))
    
    # 调用generate接口与模型交互
    #response = client.generate(model='qwen3:8b', prompt= message +ask+'/no_think')
    
    response = client.generate(
                    model='qwen3:8b',
                    prompt=message + '/no_think',  # 用户原始输入
                    system=ask,  # 系统级指令
                    options={'num_ctx': 512}  # 上下文长度控制
    )
    
    text = response.response
    cleaned_text = re.sub(r'<think>\s*</think>\s*', '', text)
    
    time.sleep(0.01)

    socket.send(cleaned_text.encode('utf-8'))
    print("消息已经发送")