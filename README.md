# Request Echo
[![Build and publish docker image](https://github.com/kassiokiarelly/request-echo/actions/workflows/docker-publish.yml/badge.svg)](https://github.com/kassiokiarelly/request-echo/actions/workflows/docker-publish.yml)

Minimal ASP.NET Core web api used for kubernetes studies containing two endpoints:

## How to run
Provided you have docker cli installed just run from your terminal:

```sh
docker run -p 8080:8080 --rm ghcr.io/kassiokiarelly/request-echo:latest
```

## /counter
With this endpoint you will get to know which instance of application your request is reaching in your cluster and how many times it got hit.

Expected response:

```json
{
    "instanceId": "9fdc45b7-3cb9-4140-a6ae-f8bc83a9db6d",
    "counter": 1
}
```

## /echo
Useful to know what are the headers sent to your application when it is behind a reverse proxy in a cluster.
```json
{
    "host": "localhost:8080",
    "path": "/echo",
    "protocol": "HTTP/1.1",
    "contentType": null,
    "contentLength": null,
    "headers": {
        "Accept": "...",
        "Connection": "...",
        "Host": "localhost:8080",
        "User-Agent": "...",
        "Accept-Encoding": "...",
        "Accept-Language": "..."
    }
}
```
