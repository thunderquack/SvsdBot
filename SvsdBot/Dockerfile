FROM ubuntu:24.04

RUN apt update -y

RUN apt upgrade -y

RUN apt install -y mc traceroute net-tools telnet htop

RUN apt install -y dotnet-runtime-8.0

RUN apt install -y ca-certificates

CMD cd /var/svsdbot && dotnet SvsdBot.dll