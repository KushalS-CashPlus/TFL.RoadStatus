# TFL.RoadStatus
A simple command line client to provide major road statuses.

### Usage

For a given valid road 
`dotnet TFL.RoadStatus.dll --road A406`
you would expect to see:

>The status of the North Circular (A406) is as follows <br> 
>Road Status is Good <br> 
>Road Status Description is No Exceptional Delays <br> 


For a given invalid road 
`dotnet TFL.RoadStatus.dll --road A1!`
you would expect to see:

>A1! is not a valid road <br> 


You can get the exit code by typing `echo $?` where 0 Represents no errors

### Assumptions

- When running the app you can provide comma separated road names like:
`dotnet TFL.RoadStatus.dll --road A406,A2` The app will display information about both roads.

- When making any http request the App Id and App Key query parameters will be set by the HttpClientHandler, by intercepting each request so we dont need to explicityly set this information everytime we implement a new http request. See https://github.com/sanganik/TFL.RoadStatus/blob/master/TFL/TFL.RoadStatus/Factories/Handlers/TflHttpHandler.cs for more information.

- When no road names are provided and then we will print all roads, with their statuses, that are available by the request `https://api.tfl.gov.uk/road/`

### Practices
- TDD - Using XUnit, and Moq

## Getting started
### Prerequisites

To run this console app you will need to have .net core installed on your machine. You can do this by following this simple guide
https://docs.microsoft.com/en-us/dotnet/core/

### Setup
1. Firstly, get your self over to the TFL developer api portal and register using the address: https://api-portal.tfl.gov.uk/login. Once you have registered login and navigate to the tab *API CREDENTIALS*. Then note down the following information
    - Application ID
    - Application Keys
2. Clone the repo
`https://github.com/sanganik/TFL.RoadStatus.git`
3. Replace appsettings.json values with your *api credentials*, this file is located in /TFL/TFL.RoadStatus.
    - Replace `<YOURAPPID>` with your application id
    - Replace `<YOURAPPKEY>` woth your application key

### Build
Navigate to TFL.RoadStatus project using terminal or command promot `cd ~/TFL/`, and let's first build the solution using the CLI command
- For windows
    - x64 `dotnet --configuration Release build win-x64`
    - x86 `dotnet --configuration Release build win-x86`
- For Mac
    - osx-x64 `dotnet build --configuration Release --runtime osx-x64` (Minimum OS version is macOS 10.12 Sierra)
    - For older versions macOS 10.10 `dotnet build --configuration Release --runtime osx.10.10-x64` where you would have to provide a specific version
    
### Test
Before we run the application lets quickly run the three tests and make sure they pass. This can be achieved by running the command `dotnet test`

### Run
On your terminal or command prompt navigate to: `cd ~TFL/TFL.RoadStatus/bin/Realease/netcoreapp2.2/`
Then to run type in the following command
`dotnet TFL.RoadStatus.dll --road {Road_Name_Goes_Here}`
