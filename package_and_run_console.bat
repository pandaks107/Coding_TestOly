@echo off
:: Set variables
set REPO_URL=https://github.com/pandaks107/Coding_TestOly.git
set IMAGE_NAME=coding_test_oly:latest
set LOCAL_FOLDER=C:\junk

:: Create the local folder if it doesn't exist
if not exist %LOCAL_FOLDER% (
    echo Creating local folder %LOCAL_FOLDER%...
    mkdir %LOCAL_FOLDER%
)

:: Clone the repository
echo Cloning repository...
if exist Coding_TestOly (
    echo Repository folder already exists. Pulling latest changes...
    cd Coding_TestOly
    git pull
    cd ..
) else (
    git clone %REPO_URL%
)

cd Coding_TestOly

:: Build the Docker image
echo Building Docker image...
docker build -t %IMAGE_NAME% .

:: Run the Docker container with interactive mode for debugging
echo Running the container in interactive mode...
docker run -it -v %LOCAL_FOLDER%:/log %IMAGE_NAME%

:: Notify user
echo Docker container execution completed. Check %LOCAL_FOLDER% for the output file out.txt.
pause
