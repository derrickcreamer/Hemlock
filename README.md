# Hemlock
*A status/attribute rule manager lib for C# games*


## What's the point of this lib?

The purpose of Hemlock is to make it easy to apply game rules to your statuses & attributes that have numerical values.

### What does that mean?

Primarily, two things:

+ You can define how each status interacts with every other status.
+ You can define what happens in response to a status changing.

### What is a status, anyway?

In our context, a status is any numerical (or boolean) piece of data that is important to the game rules.

(For example, attribute scores like Strength, Dexterity, & Wisdom are numerical, as well as Poisoned which indicates how many points of poison damage you're taking per turn.)

As mentioned, boolean true-or-false values are also perfect examples of statuses, and Hemlock is set up to handle them elegantly.

(For example, you're either Undead or not, Confused or not, Doomed or not, capable-of-opening-doors or not.)

All these statuses, attributes, and other facts & game concepts *aren't* just ints and bools. They have **rules**.

**For example, perhaps...**

+ ...you want to add a new specialized type of poison with a unique effect - and you'd prefer it if 'cure' effects automatically worked on *any* type of poison.

+ ...you want anything marked as Undead to automatically be considered Nonliving, and anything marked as Paralyzed to automatically be considered Helpless, instead of needing to set those flags separately.

+ ...you want to print a message or start an animation whenever some status changes from false to true.

+ ...you want the ImmuneToBurning status to render you, well, entirely immune to the Burning status - no matter which order they were applied in.

+ ...you want a Magic Shell effect to prevent any new spells from affecting the target, while leaving all the current spells intact & working.

+ ...you want your game's lighting system to be updated whenever an entity gains the 'Shining' status - but you need the lighting to be updated immediately, or bugs and artifacts will appear in the lighting system.

+ ...you want the iron boots to keep you grounded even if you're under the effect of a Hover spell, but you don't want to actually get rid of that hover spell - just stop it from working while the boots are on!



## When is it useful?

This lib aims to handle the types of rules mentioned above, allowing you to define them once during initialization, then have them automatically considered as you change the values of each status.

Let's say that your game has some Nonliving creatures, and some rules that care about that - perhaps a Shattering weapon deals extra damage to Nonliving creatures.

Now, what if Undead is a more specific type of Nonliving? And what if you want Holy weapons to deal extra damage only to Undead creatures? A zombie is an Undead creature, which is a type of Nonliving creature, which means that both Holy and Shattering weapons should deal extra damage to it. How can you make this work?

Normally, you'd have two options:

+ One, you could explicitly flag zombies as both Undead and Nonliving - and do this for every undead creature, being sure not to miss any!
+ Two, you could acknowledge that Undead implies Nonliving - and then the Shattering weapon would need to check whether its target is flagged as Undead *or* Nonliving.

You really want a third option - to flag zombies only as Undead, and to check only for Nonliving when a 'Shattering' weapon is used.

Hemlock provides that third option - simply declare Undead as a type of Nonliving. This type of relationship between statuses can make the bookkeeping much easier for you.

Another example:  Let's say that your game has Haste and Slow effects, and if an entity is affected by both of them, you want the latest one to win, getting rid of the other.

Normally, you'd probably check explicitly for Slow whenever Haste starts, and vice versa.

With Hemlock, you can simply declare 2 new rules:

+ Whenever Haste becomes true, cancel Slow.
+ Whenever Slow becomes true, cancel Haste.


## What existing parts will this replace?

If you currently use a `Dictionary<Status, int>` (or `<Status, bool>`), or something similar, this lib's StatusTracker is the perfect replacement.


## How is it actually used?

Let's say you've got your Status enum and your Creature class, and you want to track each Status for each instance of your Creature class.

One necessary part of setup is the definition of a converter from the Status enum to the underlying int value. This sounds silly, but unfortunately can't be avoided due to how .NET treats enums used as generics. Fortunately it is very easy:

    StatusConverter<Status, int>.Convert = x => (int)x;

With that done, let's move on to setting up some rules:

First, you create a StatusSystem object:

    var rules = new StatusSystem<Creature, Status>();

You only need one StatusSystem object - it keeps track of all the status **rules** you define.

The next step is to add all of your rules to the StatusSystem object.
Here there are only 2 rules, but see the sections below for more information about the rules you can add.

    rules[Status.Hasted].Cancels(Status.Slowed);
    rules[Status.Slowed].Cancels(Status.Hasted);

Now you're done setting up the rules - let's skip ahead to the start of the game, and the creation of some Creatures.

For each Creature that gets created, you'll need a StatusTracker object - it keeps track of the **current values** for each Status on this individual Creature.

It's important to finish adding rules before you create the first StatusTracker - as soon as you create the first StatusTracker, your rules are checked for consistency. After this step, you shouldn't go back to add or change any rules - the rules can't be changed in the middle of a game, under penalty of undefined behavior.

StatusTrackers are created by calling CreateStatusTracker on the StatusSystem object you already created.

You'll create a new StatusTracker each time you create a new Creature. You'll probably keep the StatusTracker inside the Creature:

    creature.statusTracker = rules.CreateStatusTracker(creature);

Now, when that creature gets hit with a Slowed effect, do this:

    creature.statusTracker.Add(Status.Slowed);

And finally, when it's time to check whether a creature is slowed or not:

    if(creature.statusTracker.HasStatus(Status.Slowed)){ ... }



## What are the possible relationships between two statuses?

#### A status can EXTEND another:

*StunToxin extends Poison.*  This means that StunToxin is a *type of Poison*:  StunToxin will be included whenever anything looks for Poison values.

So, if you have Poison of strength 5, and you add StunToxin of strength 2, you'll have a total Poison strength of 7 (and a StunToxin strength of 2, naturally).

Since StunToxin is a type of Poison, this also means that anything that has a negative effect on Poison (i.e. cancellation, prevention, or suppression) will have the same effect on StunToxin. So, if Poison is being prevented, StunToxin is also prevented.

#### A status can FEED another:
*ElementalOfFire feeds Burning.*  This means that ElementalOfFire adds its value to Burning, but it's *not a type of Burning.*

ElementalOfFire (which, as you might have guessed, is applied to creatures from the elemental plane of fire) isn't a type of Burning because it doesn't make sense to remove the ElementalOfFire status from a creature just because you cancelled its burning. For boolean statuses like these, it's easy to think of it as "ElementalOfFire *implies* Burning".

#### A status can PREVENT another:
*EnvironmentalFilter prevents Poison.*  This means that new instances of Poison can't be added as long as EnvironmentalFilter is true.

However, note two things:

+ First, this does not affect any Poison which might already be present.
+ Second, this only prevents the addition of instances that are of type Poison - it doesn't prevent the addition of status instances that merely *feed* Poison. Therefore, preventing a status does not guarantee that its value will never increase. (For that, see **SUPPRESS**, below.)

###### What's an instance?
A status instance simply tells the system that a specific status is present. Each status instance has an integer value which contributes to the total for the given status.

For example, if you add two instances of BonusArmor, one with value 3 and one with value 5, then your BonusArmor status will have a value of 8. The status instances are kept separate so you can remove one without affecting the others.

Additionally, note that you could choose to set BonusArmor's calculation to use 'maximum' instead of 'total'! In that case, Hemlock would use the highest value and you'd have a BonusArmor value of 5.


#### A status can SUPPRESS another:
*IronBoots suppresses Hovering.*  This means that the value of Hovering will be zero as long as IronBoots is true.

Note that suppression affects the value of the Hovering status, but *instances* of Hovering are completely unaffected. Existing instances will remain exactly where they are, and new Hovering instances can be added - but the value of Hovering will remain 0 as long as the IronBoots status is present.

#### A status can CANCEL another:
*Slowed cancels Hasted.*  This means that, whenever the value of Slowed increases, Hasted will be cancelled.

What does cancellation do? When a status is cancelled, all instances of that status (that are adding their value to that status) are removed. (Note that if a *different* status is feeding the cancelled status, that fed value will not be removed or modified - it's not an instance of this status type.)

Also note how naturally this works with statuses that are using the boolean aggregator: Since the boolean aggregator only cares about values of 0 and 1, a boolean status can only increase one time, from 0 to 1. Adding more instances of that status won't make its value increase beyond 1, which is exactly how you want it to work. Once all instances are gone, its value will finally change from 1 back to 0. (For more information about the boolean aggregator, see the next section.)

#### A status can FOIL another:
*ImmuneToBurning foils Burning.*  "Foils" is a shorthand used in Hemlock. It simply means that a status prevents, suppresses, AND cancels another status.

It shows that a status totally beats another, and is generally how you'll want to represent complete immunity to some status.

+ ImmuneToBurning suppresses Burning, because Burning absolutely cannot be true while immune.
+ ImmuneToBurning cancels Burning, because any existing instance of Burning should instantly stop when you become immune.
+ ImmuneToBurning prevents Burning, because no new Burning status should be able to start while you're immune.


## How is the value of a status calculated?

As hinted above, the value of a status is calculated by considering the value of each of its instances and any other values that are fed into this status.

By default, those values are added together to produce the total, but 'total' is only one of the built-in aggregators - boolean and maximum are also available.

If you have an instance with value 3, and an instance with value 4, and you add them both to some status...

+ ...a 'total' aggregator will result in a value of 7.
+ ...a 'bool' aggregator will result in a value of 1.
+ ...a 'max' aggregator will result in a value of 4.

The utility of total & max are probably obvious, but what about bool?

In short, it's useful because a boolean status can only increase once - from 0 to 1, false to true - before going back to 0/false. If a status is supposed to behave like a boolean, you don't want a message to get printed because that status's value increased from 1 to 2 - it's boolean, and you only care about true or false, 1 or 0.


## What happens when the value of a status changes?

When a status changes, the first thing that happens is a check for any OnChanged handlers for that status.

OnChanged handlers are defined like this:

    rules[Status.Burning].Messages.Increased = (obj, status, oldValue, newValue) => {
        messagePrinter.Print("Fire engulfs " + obj.Name + "!");
    };

This means "Whenever the value of Burning increases, this is what should happen".

There are two separate parts to this:  Messages, and Effects. What's the difference?

+ Messages happen first, and should contain notification and UI code, but shouldn't change any game state.
+ Effects happen second, and can change any part of the game state in response to the status change that just happened.

After messages & effects are applied, this status's new value is propagated to any other statuses that are affected by this one.


## What prerequisites need to be met in order to use this lib?

Your statuses need to be some kind of value type. Hemlock is designed to work perfectly with enums, but const ints (and others) will work too.

However, the (optional) text parser only works with enums. It's very handy, so please consider using it! (See the [parser syntax reference](parserReadme.txt) for more info.)

Hemlock supports using several different enums (or other value types) together - simply specify all of them as type parameters, like this:

    var rules = new StatusSystem<Creature, Status, Spell, Attribute>();

However, if two statuses have the same value, they are the same status to Hemlock.

Since enums start at 0 by default, you'll need a small bit of setup to make sure your enums don't conflict:

By default, they look like this:

    enum Status { Stunned, Poisoned, Confused };
    enum Attribute { Strength, Dexterity, Intelligence };
    enum Spell { Flight, Fireball, Charm };

Here, Stunned, Strength, and Flight share the same value, zero. To make these enums suitable for use with Hemlock, you could do this:

    enum Status{ Stunned, Poisoned, Confused };
    enum Attribute { Strength = Status.Confused+1, Dexterity, Intelligence };
    enum Spell { Flight = Attribute.Intelligence+1, Fireball, Charm };

Now, none of them are overlapping - this works great!

However, you might be worried that it's too easy to add another Status after Confused without thinking about it - a reasonable concern!

In that case, you might wish to do something like this instead:

    enum Status{ Stunned, Poisoned, Confused, LAST };
    enum Attribute { Strength = Status.LAST+1, Dexterity, Intelligence, LAST };
    enum Spell { Flight = Attribute.LAST+1, Fireball, Charm, LAST };


## What else can it do?

Once you're familiar with the basics, here are some other things this lib can do:


### Rule analysis:
If you call GetRuleErrorsAndWarnings() on the StatusSystem object, it'll tell you about any potential problems it can identify with the rules you've defined.

In addition to fatal errors, it'll warn you about strange, possibly-unintended relationships - for example, if a status cancels itself.

Calling this method at some point is highly recommended.

However, rule analysis has the potential to be slow, so for release builds - once you've addressed any errors and warnings - you should set IgnoreRuleErrors to true and avoid calling GetRuleErrorsAndWarnings().


### Custom aggregators:
'Total', 'Maximum', and 'Bool' are the built-in value aggregators, but you can define your own, too. Want to calculate the average of all the values, but only if there are at least 127 of them? You can do that.


### Value converters:
Let's say you have a Regeneration integer status, representing how much health you regain per turn, and also a TrollBlood boolean status, representing whether you're currently under the effect of magical troll blood.

You want Regeneration to increase by 5 whenever TrollBlood is true.

But, if you merely declare that "TrollBlood feeds Regeneration", it only adds 1, not 5!

The solution is to use a value converter. By default, feeds don't use value converters - they just pass the value along directly. By adding a value converter, you can change that.

A value converter takes an integer, and returns another integer. However, you usually won't need to create any value converters yourself!

In cases like this, you can just specify the value you'd like to use, and that value will be used instead of the original value:

    rules[Status.TrollBlood].Feeds(5, Status.Regeneration);

This is also how conditional relationships are handled:

    rules[Status.FatePoints].Feeds(i => i==7, Status.Lucky);
This means "FatePoints feeds Lucky whenever FatePoints equals 7".

If that isn't enough, you can also directly create a new converter. Let's say you want a status to add its value squared to another status:
    rules[Status.FatePoints].Feeds(i => i * i, Status.LuckPoints);


### Changing values with status instances:
Whenever you call CreateStatusInstance or Add on a StatusTracker, a StatusInstance is returned. Once added to a tracker, the value of that StatusInstance can be changed directly, immediately updating the status's value inside the tracker! For example:

    var statusInstance = statusTracker.Add(Status.Stunned, 20);
    statusInstance.Value -= 5;

After this, the result of `statusTracker[Status.Stunned]` will be 15.


### Single-instance statuses:
The default mode of Hemlock is to allow multiple instances per status, but there's an issue with that: With multiple instances, a statement like `statusTracker[Status.Poisoned] = 15;` doesn't make much sense. (This is why that assignment is illegal by default.)

But sometimes you want a status to act less like a collection of values (i.e., multiple instances), and more like a single integer. That's where the single-instance flag comes into play.

When you say `statusTracker[Status.Poisoned] = 15;`, you intend for the new value to be 15, no matter what was there before. This is exactly how it works once you set SingleInstance to true:

    rules[Status.Poisoned].SingleInstance = true;

Set SingleInstance to true whenever you don't want multiple instances - whenever you'll always be setting the status directly, and treating it as a single int.

It's handy if you want to use a status like this:

    statusTracker[Status.Poisoned]--;

The other features still work great with single-instance statuses. You can still respond automatically to changes, feed values from one to another, and so on.


### Status overrides:
Let's say you have a standard message *"You are stunned!"* that appears whenever the Stunned status increases.

Now let's say that you also have a StunToxin status. StunToxin is a type of poison (it extends the Poisoned status), and it also implies (feeds) the Stunned status. Therefore, whenever you get hit with StunToxin, you'll be both poisoned and stunned.

Well, let's say that you'd prefer a different message to appear whenever you get stunned because of StunToxin: *"The toxin makes your head spin!"*

You don't want *both* messages to be printed - that wouldn't sound quite right. Instead, what you want is a status override:

    rules[Status.StunToxin].Overrides(Status.Stunned).Messages.Increased =
        (creature, status, oldValue, newValue) => {
            messagePrinter.Print("The toxin makes your head spin!");
        };

What the above code means is "If a change in the value of StunToxin is responsible for the value of Stunned being increased, override its usual 'increased' message with this new one."

What if the Poisoned status also has its own message? If you want the new message to replace both the Stunned and Poisoned messages, you'll also want to override the Poisoned message, and this time you want it to do nothing at all:

    rules[Status.StunToxin].Overrides(Status.Poisoned).Messages.Increased = null;

By doing this, no 'Poisoned' message will be printed whenever StunToxin is being applied.


### Override sets:
If you have several OnChanged handlers you'd like to group together and reuse for multiple different statuses, use override sets:

    rules.GetOverrideSet(0).Overrides(Status.Burning).Effects.Decreased =
        (creature, status, oldValue, newValue) => { /*Overridden Burning handler code here*/ };
    rules.GetOverrideSet(0).Override(Status.Freezing).Effects.Decreased =
        (creature, status, oldValue, newValue) => { /*Overridden Freezing handler code here*/ };

    rules[Status.Numb].UsesOverrideSet(0);
    rules[Status.Ethereal].UsesOverrideSet(0);

Now both Numb and Ethereal statuses will use the handlers from the override set. Note that if a status is using an override set, Hemlock will ignore any handlers defined directly on that status and use only the handlers on the override set.

(You can choose any integer to refer to an override set; there are no requirements to start at zero or maintain any kind of order.)

Override sets also allow you to customize messages and effects in another powerful way: instance overrides.


### Instance overrides:
The concept behind status overrides (above) can also be applied to individual status instances. Here's an example:

Let's say that you have a standard message *"You're on fire!"* that appears whenever the Burning status increases.

Now let's say that, whenever a wand of immolation is the cause of the Burning status, you want a different message: *"The wand's beam sets you on fire!"*

Just like before, you don't want to print both messages, but unlike last time, this isn't one status overriding another status.

What you want in this situation is an instance override:

    rules.GetOverrideSet(42).Overrides(Status.Burning).Messages.Increased =
        (creature, status, oldValue, newValue) => {
            messagePrinter.Print("The wand's beam sets you on fire!");
        };

    creature.statusTracker.Add(Status.Burning, overrideSetIndex: 42);

By setting the overrideSetIndex on the newly created StatusInstance, if the creature's Burning status increases when the new instance is added, it'll use override set 42 instead of the usual handlers for Burning.


### Passive prevention:
This one is conceptually simple, but not really a core feature, so this section appears all the way down here.

You might want some statuses to be prevented based on arbitrary conditions - conditions that have nothing to do with the status system.

For example, you might want to prevent Burning for any creature that is currently in water:

    rules[Status.Burning].PreventedWhen(
        (creature, status) => creature.IsCurrentlyInWater()
    );

This is useful for prevention conditions, because (unlike suppression and cancellation) prevention only matters at the exact moment when a status instance is added.


### Cancel priority
Normally, when a status is canceled, all the StatusInstances for that status are removed in insertion order, with the value being recalculated as each is removed.

Occasionally, it might be useful to have more control over the order in which instances are removed, especially for a boolean status, and especially if each instance prints a different message (through use of the override set feature).

You can set the CancelPriority property on a StatusInstance to control this behavior: StatusInstances will be removed in order of CancelPriority, lowest to highest (while preserving insertion order for instances with the same priority).


### Derived statuses
In the rare case where you have a status that can't be calculated with a normal aggregator (one where `int[] -> int` is not specific enough), you can make that status into a derived status. If your game has a numerical value that requires complex logic to be applied to several other statuses (but you still want it to be a status you can check), derived statuses might fit your need.

The value of a derived status is based entirely on the values of some other statuses; a derived status does *not* participate in any of the usual status relationships in the normal way: it can't feed or be fed, prevent or be prevented, etc., nor can you add instances of a derived statuses directly.

In order to turn a status into a derived status, simply set its `CalculateDerivedValue`. Here's an example where we determine the loot rating of a random encounter based on some other numbers:

    rules[EnemyEncounterStatus.LootRating].CalculateDerivedValue = baseTracker => {
        var tracker = (StatusTracker<MyGameObj, EnemyEncounterStatus, QuestStatus>)baseTracker;
        if(tracker[EnemyEncounterStatus.BaseDifficultyRating] < 3){
            return 1; // Only basic loot for easy encounters
        }
        // Give better loot based on the number of quests completed:
        int result = tracker[EnemyEncounterStatus.BaseDifficultyRating] + (tracker[QuestStatus.QuestsCompleted] / 3);
        return Math.Min(result, 10); // Loot rating is capped at 10 regardless of quests completed
    };

As you can see from this example, derived statuses are appropriate when the final value depends on treating different statuses differently during calculation (unlike the `int[] -> int` aggregators used for most statuses).

Getting the value of a derived status works just like any other: `int lootRating = myTracker[EnemyEncounterStatus.LootRating];`


## Serialization

See the [serialization guide](serializationGuide.md).


## How does the parser work?

The parser takes a filename or a sequence of strings and applies the rules defined in that text to a StatusSystem object.

Take a look at the [parser syntax reference](parserReadme.txt).
It's meant to be simple to use, but powerful enough to cover most of the common relationships between statuses.

After the parser has created the relationship rules between statuses, you can add OnChanged handlers for messages & effects just like you normally would.


## Advanced questions

#### How should one decide whether to use 'extends' or 'feeds'?

Well, let's say that PermanentlyBlind feeds Blinded.  This means that PermanentlyBlind transfers its value over to Blinded, but it's *not a type of Blinded.*

That's strange - it certainly sounds like a type of Blinded, doesn't it?

The difference, and the answer to that question, lies in what should happen when something tries to cancel (or suppress, or prevent) the Blinded status.

Let's say that potions of healing cure blindness, but shouldn't cure permanent blindness.

If PermanentlyBlind was a type of Blinded (if "PermanentlyBlind extends Blinded"), then you wouldn't be able to get rid of Blinded without also getting rid of PermanentlyBlind.

So, by making PermanentlyBlind feed Blinded, that problem is avoided.


#### What makes some rock-paper-scissors suppression cycles dangerous, but not others?

For example, if 3 statuses only suppress each other, that's illegal.

And, when 3 statuses suppress and cancel each other, that's okay...most of the time.

What causes this behavior?

The first situation is illegal because of what would happen if something added to the value of all three of those statuses. Add Rock first. Then add Paper, which suppresses Rock. Then add Scissors, which suppresses Paper - but now that Paper has a value of zero, Rock is no longer suppressed! Since Rock is back, Scissors becomes suppressed - and Paper is no longer suppressed. Paper suppresses Rock again and we're stuck in an infinite loop!

The second situation is okay, *so long as the rules can guarantee that the infinite loop won't happen.* If any of the three statuses cancels the next (in addition to the suppression), then the cancelled one won't be present on the next loop. Infinite loop averted! The exception to this rule happens when that status is *fed* (by any status). If it's fed, then the cancellation is *not* guaranteed to reset it to zero, and the infinite loop might still happen.

#### How hard is it to do X?

Take a look at some [advanced examples](advancedExamples.md), like how to model two statuses that oppose one another.

### *Thanks for reading!*
##### *Any questions? You can email me at this same username on Gmail.*
