# WaitForDocker
This project helps with automating integration tests of services using docker. It will run your `docker-compose.yml` infrastructure file and will wait until your services will be ready to receive calls. After finished tests, you can easily remove your services by killing them.

# Installation
```Install-Package WaitForDocker```
# Prerequisites
```docker-compose.yml``` which contains your infrastructure setup
# Usage
```csharp 
await WaitForDocker.Compose();
```
# Configuration
`WaitForDocker` will look for ```docker-compose.yml``` in current directory so marking this compose file as copy always is required. If you want to use different directory or compose file name you can use config builder.
```csharp
var config = new WaitForDockerConfigurationBuilder()
                 .SetComposeDirectoryPath("path")
                 .SetCustomComposeFileName("filename")
                 .Build();
```
# Health checking
By default `WaitForDocker` will read `services` tag and they `ports` from `docker-compose.yml` and check that port is open.
```
services:
  redis:
    image: redis
    ports:
      - '6379:6379'
```
If this is not enough you can specify your services health checks. At this moment there is 3 health checks. `HTTP`,`TCP` and `CMD` but you can create custom ones.  
```csharp
 var config = new WaitForDockerConfigurationBuilder()
                  .AddHealthCheck(check => check.WithHttp("rabbitmq", new Uri("http://localhost:15672")))
                  .AddHealthCheck(check => check.WithCmd("rabbitmq", "rabbitmqctl status"))
                  .AddHealthCheck(check => check.WithCustom(logger => new SomeHealthCheck("sqlserver", 100, null, logger)))
                  .Build();
```
For create custom health check you have to inherit from ```csharp DockerHealthChecker``` class and pass it to `WithCustom` method.
Each health check have a defult timeout set to `10` seconds, but you can specify any value you want.

# Logging
For setup custom logger you have to implement ```ILogger``` interface and pass it to config. By default logs will be printed to the console.
```
var config = new WaitForDockerConfigurationBuilder()
                .SetCustomLogger(new FileOutputLogger())
                .Build();
```
