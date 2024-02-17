# Setting Up .NET Project

Follow these instructions to set up the .NET project:

1. **Clone Repository**: 
   ```bash
   git clone https://github.com/edwinwkuria/Cards.git
2. **Clone Repository**: 
    ```bash
   cd Cards
3. **Download the .NET SDK for .NET6**: 
    Download and install the sdk for .net 6.
4. **Install Required Packages for Cards.API:**
    ```bash
    cd Cards.API
    dotnet restore
5. **Install Required Packages for Cards.Infrastructure:**
    ```bash
    cd ../Cards.Infrastructure
    dotnet restore
    cd ../
6. **Create Database and Update Appsettings:**
    Create a database named Logicea.
    Update the database credentials in Cards.Api/appsettings.Development.json.
7. **Update database**
    ```bash
    dotnet ef database update --project Cards.Infrastructure --startup-project Cards.API --context DatabaseContext
8. **Build the application**
    ```bash
    dotnet build
9. **Run the application**
    ```bash
    dotnet run --project Cards.API
10. **Access the application APIs**
    ```bash
    dotnet run --project Cards.API


