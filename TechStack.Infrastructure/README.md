CRONS EXPRESSION

# field #   meaning        allowed values
# -------   ------------   --------------
#    1      minute         0-59
#    2      hour           0-23
#    3      day of month   1-31
#    4      month          1-12 (or names, see below)
#    5      day of week    0-7 (0 or 7 is Sun, or use names)
Instead of the first five fields, one of eight special strings can be used :

string         meaning
------         -------
@reboot        Run once, at startup.
@yearly        Run once a year, "0 0 1 1 *".
@annually      (same as @yearly)
@monthly       Run once a month, "0 0 1 * *".
@weekly        Run once a week, "0 0 * * 0".
@daily         Run once a day, "0 0 * * *".
@midnight      (same as @daily)
@hourly        Run once an hour, "0 * * * *".