#!/usr/bin/env pwsh

docker build -t prometheus-example-webapi -f .\prometheus-example\Dockerfile .
docker build -t prometheus-example-host -f .\prometheus-example-host\Dockerfile .
# docker build -t prometheus-example-collector -f .\prometheus-collector\Dockerfile .

Push-Location .\devtools
    docker-compose up -d
Pop-Location

docker run --rm --name prometheus-example-collector -v ./prometheus-collector:/prometheus --network devtools_default bitnami/prometheus:latest --config.file=/prometheus-collector/prometheus.yml
