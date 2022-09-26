## 名称解释
- Service：对应着后端的一个服务，或者是一个App。
- Route：路由，不同的route对应着service中的不同接口。
- Upstream：和nginx中的upstream擦不多，都对应着一组服务节点。
- Target：对应着一个api服务节点。

```
# upstream 对应kong中的Upstream
upstream default_backend {
  # server 对应着Kong中的一个target
    server 10.4.4.21:81    max_fails=3 fail_timeout=10s;
    server 10.4.4.22:81    max_fails=3 fail_timeout=10s;
}

server {
    server_name *.bairimeng.com;
    # location / 对应着Kong中的一个route
    location / {
        proxy_pass http://default_backend;
        proxy_set_header Host       $http_host;
        proxy_set_header x-forwarded-for $proxy_add_x_forwarded_for;
    }
}
```

## How TO Start
1. 根目录执行 `docker-compose up -d` 启动 Kong 和 Konga
2. 浏览器打开 localhost:1337 ,初始化 Konga 用户
3. 在 Snapshot  导入 docs 目录下 snapshot.json，导入后点击 详情页面，然后 Restore
4. 修改 host 文件 
你的IP sample.api.com
5. postman 导入 Kong.postman_collection.json
6. 同时启动三个项目，获取 token 后，调用接口

