#!/usr/bin/env pwsh

docker build -t stephenwike/prometheus-example-host:latest -f .\prometheus-example-host\Dockerfile .
docker push stephenwike/prometheus-example-host:latest

