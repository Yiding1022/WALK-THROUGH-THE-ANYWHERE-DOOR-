# -*- coding: utf-8 -*-
"""
Created on Sat May 24 20:40:43 2025

@author: DONG
"""

import ollama
import re
# 创建客户端
client = ollama.Client(host='http://localhost:11434')

# 拉取模型（以通义千问2的0.5b版本为例）
client.pull("qwen3:0.6b")

# 列出已下载的模型
models = client.list()

# 调用generate接口与模型交互
response = client.generate(model='qwen3:0.6b', prompt='编一个故事？不超过100字。/no_think')
text = response.response
cleaned_text = re.sub(r'<think>\s*</think>\s*', '', text)
print(cleaned_text)