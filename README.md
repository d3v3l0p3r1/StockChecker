# StockChecker

##Instructions:
In the shared folder you will find a zipped Visual Studio solution.

In the solution you will find a console application which is extracting data which indicates the most mentioned stocks on Reddit for use in predicting trader sentiment.

This is part of a process which is scheduled in windows task scheduler. The console application is invoked and saves all of the data for the second quarter of the year.

This process has some issues. It has been reported that it crashes sometimes, it’s slow, some data seems to be missing, and the database is growing more than expected.

The database (faked in the code) contains a table which contains tickers and another table which contains the data (by day) for each of those tickers. 
It’s a normalized database structure.

As a technical lead on your team it’s your job to show the developer who wrote this code what can be improved – everything from code structure, 
the code itself, ideas for improved architecture of the entire solution, and logical bugs which may need to be fixed.

Make at least the most critical improvements to the code as needed. (including bug fixes as well as important structural changes) 
If you’re inclined to show all of your ideas in code, change however much you require.
Provide a summary of your code changes as well as future improvement ideas for the developer.

##Initial implementation
The initial implementation is written in a procedural style. The procedural style can be used if it is proof of conept. if the program will always be used, then you will need to make changes. Making changes to the program is more difficult. It is much easier to make changes to OOP-style programs.

##My implementation
I have identified 3 main entities.

    ###DAL
	The level of access to the data. In the IRepository abstraction, I encapsulate all the logic for storing and changing data.
	
	###API client
	In the IRedditClient, I encapsulate the logic of the request in an external system. Polling has also been added to this client. A retry and request rate limit has also been added to this client.
	
	###Processors
	The processor contains logic for processing data from IRedditClient and also storing data in IRepository.	