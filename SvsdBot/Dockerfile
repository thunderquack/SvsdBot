FROM centos:9

RUN yum update -y

RUN yum install -y https://dl.fedoraproject.org/pub/epel/epel-release-latest-9.noarch.rpm

RUN yum install -y mc traceroute net-tools telnet htop

RUN rpm -Uvh https://packages.microsoft.com/config/centos/9/packages-microsoft-prod.rpm

RUN yum install -y dotnet-runtime-8.0

RUN dotnet --info

CMD cd /var/svsdbot && dotnet SvsdBot.dll