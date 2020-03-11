#!/usr/bin/env pwsh

docker build -t prometheus-example-webapi -f .\prometheus-example\Dockerfile .
docker build -t prometheus-example-host -f .\prometheus-example-host\Dockerfile .

Push-Location .\devtools
    docker-compose up -d
Pop-Location

