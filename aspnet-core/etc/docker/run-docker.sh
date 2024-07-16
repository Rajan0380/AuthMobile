#!/bin/bash

if [[ ! -d certs ]]
then
    mkdir certs
    cd certs/
    if [[ ! -f localhost.pfx ]]
    then
        dotnet dev-certs https -v -ep localhost.pfx -p 8d35beff-d8d3-44d4-bf2b-f9d9872cbd0d -t
    fi
    cd ../
fi

docker-compose up -d
