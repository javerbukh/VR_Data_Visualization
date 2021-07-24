.. _quickstart:

Quickstart
==========

In the ``player`` script, go to the ``Update()`` method and then to the if statement
that checks if the "Q" key has been pressed. There, you fill find the line::

    string[] input_files = new[] { "" }

Inside the quotation marks, you can put the location of the file you want to load.
Make sure the first line of the file starts with "# X Y Z". It must include "X Y Z"
in the first row.

Next, go to Unity and click the ``LoadDataPrefab``. Here, you can set graph options
for loading your data. For data sets under 20,000 points, I would recommend having
the ``OBJECT_SCALE`` set to 0.3 and ``GRAPH_SCALE`` at 100.
