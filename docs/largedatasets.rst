.. _largedatasets:

Load large data sets
====================

Loading data sets over 100,000 points is not recommended using this application,
but it is possible.

To do so, use the following formula::

    ((X / (0.03182)) / (4/3 * pi)) ^ (1/3)

Where X is the number of data points to determine the ideal graph scale to visualize that data.
Then, go to the ``LoadDataPrefab`` and set the ``GRAPH_SCALE`` to equal the value of X*2.
