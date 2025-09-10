# Phi3 PoC Project


Simple dotnet 8 asp net project that uses Phi 3 Model (https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-onnx) to generate text


### Features

Minimal Api

Uses Phi 3 mini (cpu-int4-rtn-block-32-acc-level-4) model

Eager loading of the model on start up though background service

Api key authentication


### Endpoints

/api/generate

/ping

### Publish the project

git clone https://github.com/3-gin-7/Phi3PocApi.git

cd Phi3PocApi

dotnet publish -c Release -r win-x64 --self-contained false -p:ContinuousIntegrationBuild=true -o ./publish

copy the publish folder to VM

cd publish

dotnet .\Phi3PocApi.dll


### Hosting notes for deployment to windows VM

#### VM Requirements

* Phi3 Model (if not present on the VM already)

- Python (needed to install huggingface cli, https://www.python.org/downloads/windows/)

- Huggingface cli - pip install -U "huggingface_hub[cli]"

- Install model into AIModel folder:

    * cd AIModel
    * huggingface-cli download microsoft/Phi-3-mini-4k-instruct-onnx --include cpu_and_mobile/cpu-int4-rtn-block-32-acc-level-4/* --local-dir .

* Runtime

- dotnet runtime - https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-8.0.20-windows-x64-installer?cid=getdotnetcore

- aspnet core runtime - https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.19-windows-x64-installer

- VC++ - https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist

* Firewall

New-NetFirewallRule -DisplayName "Phi3Api-80" -Direction Inbound -Protocol TCP -LocalPort 80 -Action Allow
