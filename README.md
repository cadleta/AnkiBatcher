# AnkiBatcher

AnkiBatcher is a command-line utility that processes quiz text files and converts them into formats suitable for importing into [Anki](https://apps.ankiweb.net/), a popular flashcard application. It supports various question types such as multiple-choice, true/false, and cloze deletions.

## Features

- **Multiple Note Types**: Choose from All Multiple Choice, Multiple Choice & True/False Cloze, Advanced Cloze, Basic, or Dynamic.
- **Multiple Modes**: Select modes like Self Quiz Decoder, Definition Researcher, Book Quotes, or Based On Note Type.
- **Customizable Output**: Specify your desired output file name and location.

## Prerequisites

- **.NET Framework** or **.NET Core** installed on your machine.
- A C# compiler if you need to build from source.

## How to Build

1. **Clone or Download the Repository**: Obtain the source code from the repository.
2. **Open the Solution**: Use Visual Studio or any C# compatible IDE to open the `AnkiBatcher.sln` file.
3. **Build the Solution**: Compile the project to generate the executable.

## How to Use

1. **Open Command Prompt or Terminal**: Navigate to the directory containing the compiled `AnkiBatcher.exe`.
2. **Run the Program**: Type `AnkiBatcher.exe` and press **Enter**.
3. **Follow On-Screen Prompts**:

   - **Enter Text File Path For Input**: Provide the full path to your quiz text file.
   - **Enter Output File Name**: Specify the desired name for your output file (without extension).
   - **Select Note Type**: Choose a note type by entering the corresponding number:
     - `0` - All Multiple Choice
     - `1` - Multiple Choice & True/False Cloze
     - `2` - Advanced Cloze
     - `3` - Basic
     - `4` - Dynamic
   - **Select Mode**: Choose a mode by entering the corresponding number:
     - `0` - Self Quiz Decoder
     - `1` - Definition Researcher
     - `2` - Book Quotes
     - `3` - Based On Note Type

4. **Processing**: The program will read the input file, process the data based on your selections, and generate an output file.
5. **Import into Anki**: Use Anki's import feature to add the generated cards to your deck.

## Notes

- **Input File Format**: Ensure your quiz text file follows the expected format for accurate processing.
- **Output Location**: The output file will be saved in the same directory as your input file unless otherwise specified.

## Example

```bash
C:\AnkiBatcher> AnkiBatcher.exe
Enter Text File Path For Input:
C:\Quizzes\sample_quiz.txt
Enter Output File Name:
anki_cards
What Note Type Would You Like?
Enter the number for the corresponding mode:
0 - All Multiple Choice
1 - Multiple Choice & True/False Cloze
2 - Advanced Cloze
3 - Basic
4 - Dynamic
0
What Mode Would You Like?
Enter the number for the corresponding mode:
0 - Self Quiz Decoder
1 - Definition Researcher
2 - Book Quotes
3 - Based On Note Type
0
```

## Disclaimer
This project is no longer maintained, and I will not be making any further changes or updates.
