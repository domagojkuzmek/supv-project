## Migrations commands

Run the commands from supv directory

- Create migration
dotnet ef migrations add InitialCreate --project .\src\supv.Data\supv.Data.csproj --startup-project .\src\supv.API\supv.API.csproj

- Apply migrations to database
dotnet ef database update --project .\src\supv.Data\supv.Data.csproj --startup-project .\src\supv.API\supv.API.csproj

## Git guidelines

- in a commit describe in simple terms what changes 
- the commit message should provide all necessary details
- examples: 
        "Add test for expired access token."
        "Update user response with account status."

    Branches

    Long-living branches

    - develop, latest code in the development
    - release, code of a particular release
    - main, PROD environment code

    Short living branches
    - feature
    - hotfix

branching workflow:
    - after configuring and setting up, main gives a stable start point
    - from that state create branch "develop", new work starts from this branch
    - from develop create your feature branches, when feature finsihed merge into develop
    - each feature branch focus on one logical piece of work
    - when develop contains everything you want in a release, create a release branch
    - use release for testing and fixing, when approved merge into main

