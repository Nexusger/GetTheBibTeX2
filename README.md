Dblp
============

This project aims to provide a simple api to get BibTex files for papers, journals, conference proceedings and so on.

I try to reach this goal with the following approach:
- Get public available data (for example: http://dblp.uni-trier.de/)
- Transform the data
- Create a .NET WebAPI endpoint which provides the data
- Create a .NET MVC frontend which grabs the WebAPI Data and provides a way to download the cummulated BibTex files
