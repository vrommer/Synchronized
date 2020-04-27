FROM mcr.microsoft.com/dotnet/core/sdk:2.1
# Install nodejs
RUN curl -sL https://deb.nodesource.com/setup_12.x -o nodesource_setup.sh
RUN chmod 755 nodesource_setup.sh
RUN ./nodesource_setup.sh
RUN apt-get update
RUN apt-get install nodejs
# Setup Project
RUN git clone https://github.com/vrommer/Synchronized.git
COPY credentials.properties.xml Synchronized/Synchronized.WebApp/.
WORKDIR Synchronized/Synchronized.WebApp
RUN npm install
WORKDIR ..
RUN dotnet publish
WORKDIR Synchronized.WebApp/bin/Debug/netcoreapp2.1/publish/
CMD dotnet Synchronized.WebApp.dll
