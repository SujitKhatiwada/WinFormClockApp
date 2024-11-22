# Windows Forms Clock Application

This is a simple Windows Forms application built using C# and the .NET Framework. The application displays the current local time on a label that continuously updates every second, just like a clock.

## Key Features:
- Displays the current local time in a 12-hour format with AM/PM.
- Continuously updates the label every second using a background worker.
- Implements proper thread synchronization to update the UI from the background thread.
- Supports cancellation of the background worker when the application is closed.

## Technologies Used:
- **C#** (Programming Language)
- **.NET Framework** (Windows Forms)
- **BackgroundWorker** (For background processing)
- **Thread.Sleep** (For delaying updates)

## Project Structure:
- `Form1.cs`: Contains the logic for the Windows Form, including the background worker that updates the time.
- `Form1.Designer.cs`: Contains the design elements of the form, including the `Label` control that displays the time.
- `Program.cs`: The entry point of the application, responsible for starting the form.

## How to Run:
1. Clone this repository to y
