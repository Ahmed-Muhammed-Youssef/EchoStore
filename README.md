# Getting Started
* Install [.Net 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).
* Install Redis
  * For Windows, I recommend installing WSL (Windows Subsystem for Linux), from [HERE](https://learn.microsoft.com/en-us/windows/wsl/install). 
and install [Redis](https://redis.io/docs/getting-started/installation/install-redis-on-linux/) on the linux distro you choose. 
* Launch the Redis server by typing the `redis-server` command in the Linux Shell.
* Clone the project, then open up you terminal, go to the `EchoStore/API/` path and type `dotnet run` and press enter.
* Open up your favorite browser, and navigate `https://localhost:5001/swagger/index.html`.
Note: the 5001 port number in the url depends on your OS, the port that the asp.net core app listens on, appears after you execute the `dotnet run` command.
* Navigate the APIs using swagger UI.
