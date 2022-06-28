
# 此分支版本文档

## 新增功能
- 允许点免费电台节目， 格式"点歌 电台 数字id"
- TODO 尝试优化结构 

## 关于CefSharpEmbedded分支修改登录流程的说明
网易云增加了强制性滑块登录验证流程，所以原喵块采用的发登录请求方法会报错“未知服务器返回”。   
1.2.0版本修改了插件登录步骤。汪块现在内置了CefSharp基于Chromium的浏览器，从而允许用户采用常规网易云网页登录操作来获取必要的Cookie。  
由于本人不是专业的C#桌面端开发者，所以这个解决方案比较粗暴，效率并不高。  
注意，`master`分支下仍采用原来喵块的设计，故无法登录。

# 原始文档

## 插件功能
---
- 不再请求lwl12的api，改为本地发送请求，减轻api的压力
- 允许用户添加空闲歌单
- 允许用户登录网易云账号。若账号拥有音乐包/会员，点歌品质最高可至320Kbps

由于C#没有RSA_NO_PADDING这种填充方法,插件借助了BouncyCastle进行加密

项目的接口及加密方法均参照 [Binaryify的NeteaseCloudMusicApi项目](https://github.com/Binaryify/NeteaseCloudMusicApi) 进行翻译
