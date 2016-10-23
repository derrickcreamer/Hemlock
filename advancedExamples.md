## Haste vs. Slow

#### 3 ways to handle this classic clash of statuses

Many games have Haste and Slow effects, and you'll find that they handle the opposition of these two statuses in a few different ways.

Mainly, there are 3 interactions you'll see:  either "first one wins", "last one wins", or "they neutralize each other".

Whichever you choose, all three of these relationships are easy to create in Hemlock.


### First one wins

Here, you want the one that becomes true *first* to prevent the other from ever becoming true:

    rules[Status.Haste].Prevents(Status.Slow);
    rules[Status.Haste].Suppresses(Status.Slow);

    rules[Status.Slow].Prevents(Status.Haste);
    rules[Status.Slow].Suppresses(Status.Haste);

Easy enough - the prevention prevents new sources from being added, and the suppression prevents the value from ever becoming true, no matter what.

### Last one wins

This one is even easier:

    rules[Status.Haste].Cancels(Status.Slow);

    rules[Status.Slow].Cancels(Status.Haste);

### Neutralization

This one is slightly more complex. A common mistake in this case would be to have them suppress one another. (That doesn't work because, after the first is true, the second can't ever become true.)

Instead, you need to create 2 extra statuses. Now you'll have one status to indicate whether a Haste *effect* is present, and another status to indicate whether you're actually *Hasted*.

    rules[Status.HasteEffect].Feeds(Status.Hasted);
    rules[Status.HasteEffect].Suppresses(Status.Slowed);

    rules[Status.SlowEffect].Feeds(Status.Slowed);
    rules[Status.SlowEffect].Suppresses(Status.Hasted);

Notice that HasteEffect and SlowEffect don't affect each other directly - they'll happily exist side-by-side. When both HasteEffect and SlowEffect are true, both Hasted and Slowed will be suppressed, giving you the neutral speed you desire.

