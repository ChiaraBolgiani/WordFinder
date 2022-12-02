# WordFinder

WordFinder is an application to look for words in a specified folder. The application allows to perform multiple searches of a single word and returns the top ten files with the highest occurrence of the desired word or a suitable message if the word is not found. 

### Build the applciation
The application was created with Visual Studio 2022 and .NET 6.0.
Download the source code and build the application  from Visual Studio 2022 or navigate to the folder containing with the command:
```sh
dotnet build
```
### Run the application
Download the WordFinder.zip folder from the Release tagged v1.0. From the command line navigate to the folder and then run the executable:
```sh
WordFinder.exe <path_to_text_files> 
```
Alternatively, run the application from Visual Studio 2022 providing the path to the text files as command line arguments or by navigating to folder containing WordFinder.csproj and executing the command:
```sh
dotnet run -- <path_to_text_files> 
```

### Usage
The application will not work if a path to the files is not given as command line argument. 
Once started, the application will give a prompt to search for a word and will show the top ten files containing that word (or less, if less than ten files with that word are found) with the amount of times the word appears in each document. If no files contains the word a proper message is displayed.
The search can be repeated with another word until the users presses Ctrl-C.

##### Example

```sh
WordFinder.exe C:\Users\user\TestData
```

The application will output:
```sh
Found 12 text files.

----WordFinder----

Search:
```

The user can then input a single word. e.g. "coffee"

```sh
Search: coffee
```

And the application will output:
```sh
Word found in the following file(s):
Coffee1.txt : 19
Coffee2.txt : 16
Coffee3.txt : 12
Coffee4.txt : 10
Coffee5.txt : 10
Coffee6.txt : 9
Coffee7.txt : 9
Coffee8.txt : 7
Coffee9.txt : 5
Coffee12.txt : 3

Press Ctrl-C to stop searching.
```
