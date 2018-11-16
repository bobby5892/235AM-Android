This app gives you practice using an SQLite Database and SQLite.Net ORM. This is a completed version of a student exercise. Students used the DataAccessDemo app as a starter app, then added three more stock data files to the console app, a Spinner and DatePicker with queries and a more complex query to the Android app.

There are three projects in the solution:
* DAL - Data Access Layer. A .NET Library project that contains the data model for the ORM and the SQLite.NET package (just one source code file). The two files in this project are linked in the DAL folders of the other two projects.
* Console - A .NET Console app that parses Stock data files (csv) and puts the data into a SQLite database file in the Assets folder of the Android project. Note: this project requires the 32 bit [SQLite3.dll](http://sqlite.org/index.html).
* Android - An Android app that has a Spinner for selecting a stock symbol, a DatePicker for choosing a beginning date, and a ListView to display the stock prices.

Note: This app is just one app in the class demo repository.
To get the source code for this app you will need to
clone or download the whole repository.

This demo was written for CS235AM, Android App Development, at Lane Community College.
Read more about the course here: https://birdsbits.wordpress.com/2014/09/09/xamarin-android-course
