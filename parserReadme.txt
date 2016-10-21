# Hemlock parser syntax

# Here's the enum we'll be using:  enum PhoneticAlphabet { Alpha, Bravo, Charlie, Delta, Echo,
#     Foxtrot, Golf, Hotel, India, Juliett, Kilo, Lima, Mike, November, Oscar, Papa, Quebec,
#     Romeo, Sierra, Tango, Uniform, Victor, Whiskey, XRay, Yankee, Zulu };

# This file is a valid input to the parser! (although the resulting rules won't make much sense)

# Here we go:

alpha extends bravo         # This is equivalent of calling "rules[PhoneticAlphabet.Alpha].Extends(PhoneticAlphabet.Bravo);"

charlie foils delta         # Use '#' to start a comment and ignore the rest of the line (you've probably already figured that out).

echo prevents foxtrot, golf, hotel        # Use commas to specify multiple relationships at once.

india prevented by juliett, kilo    # You can flip the verbs to create multiple relationships in reverse.
lima fed by mike

november {                  # Use braces to start a block.
  cancels oscar
  feeds papa                # The rules inside this block will all be applied to November.
  feeds quebec
}

romeo{
  feeds sierra;  suppresses tango;  cancels uniform          # Semicolons can be used instead of line breaks.
}

victor feeds whiskey;  XRAY prevents yankee;  Zulu fed by alpha   # Names aren't case-sensitive.


bravo feeds charlie 5         # "if Bravo is greater than 0, feed Charlie a value of 5"
charlie 5 fed by bravo        # (Exactly the same rule as the previous line, just written backwards.)

delta>=5 prevents echo        # "if Delta is greater than or equal to 5, prevent Echo"

foxtrot == 2 feeds golf 7     # "if Foxtrot is exactly 2, feed Golf a value of 7"

hotel {
  suppresses india
  bool                        # "assign the Bool aggregator to Hotel"
  suppresses juliett          # Recognized aggregators are "total", "bool", and "max".
}

bool:          # This line is important: It sets the parser to "bool" aggregator mode.
               # Every status that is listed FIRST in a rule, from this point on,
               # will be set to the Bool aggregator.

kilo; lima; mike;              # Here, Kilo, Lima, and Mike are set to "bool".

november foils oscar           # Note that Oscar is *NOT* set to "bool" here. November is the primary status because it's listed first.

oscar foiled by november       # For this line, Oscar is set to Bool, not November. The verb doesn't matter - it only matters which status appears first.

total:             # This line puts the parser back in "total" mode, which is the default unless you changed it on the StatusSystem object.

single:        # In addition to Total/Bool/Max modes for aggregators, there is also Multiple/Single.
               # By enabling "single" mode, each status that begins a rule will have SingleSource set to true.
papa suppresses quebec     # Again, this marks Papa as SingleSource, but doesn't affect Quebec.

# That's all!
