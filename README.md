
# Coding_TestOly

A simple console application that demonstrates writing logs to a file inside a Docker container. This project includes an automation script to package, build, and run the application in a Linux-based Docker container.

---

## Features

- Console application written in **C#** using **.NET 6.0**.
- Packages the application into a **Linux-based Docker container**.
- Automatically generates a log file (`output.txt`) in a specified host-mounted directory (`C:\junk`).
- Includes an automation script (`package_and_run_console.bat`) for simplified execution.

---

## Prerequisites

Before using this project, ensure you have the following installed:

1. [Docker Desktop](https://www.docker.com/products/docker-desktop) (Ensure it's running in **Linux mode**).
2. [Git](https://git-scm.com/) for cloning the repository.
3. Windows OS (for `.bat` execution).

---

## Getting Started

Follow these steps to clone, build, and run the project.

### Step 1: Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/pandaks107/Coding_TestOly.git
cd Coding_TestOly

or run package_and_run_console.bat file 


Coding_TestOly/
│
├── Coding_TestOly.csproj       # Project file for .NET
├── Program.cs                  # Main application code
├── Dockerfile                  # Dockerfile to build the image
├── README.md                   # Documentation
├── package_and_run_console.bat # Automation script for building and running
├── output.txt (Generated)      # Log file (generated after running)

