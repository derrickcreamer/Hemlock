## Serialization


### Basic guidelines for Hemlock serialization:

1) StatusSystem is not ever serialized - it should be recreated on game start.

2) Each StatusTracker can be serialized and deserialized independently of any other StatusTracker. (Useful if you need to load/unload chunks or levels on the fly.)

3) All StatusInstances inside a StatusTracker will be serialized along with it. (Hemlock provides a way to maintain associations from each StatusInstance to the rest of your game - see below.)

4) StatusInstances that don't currently belong to a StatusTracker can be serialized and deserialized whenever you choose.


## Quick examples:

#### StatusTracker
Assuming your StatusSystem object is named 'statusRules', you want to serialize a single creature's status tracker, and you already have the necessary System.IO.Stream (or BinaryWriter/BinaryReader):

    creatureBeingSerialized.statusTracker.Serialize(saveStream);

To deserialize later, create an empty StatusTracker, then call its Deserialize method:

    creatureBeingDeserialized.statusTracker = statusRules.CreateStatusTracker(creatureBeingDeserialized);
    creatureBeingDeserialized.statusTracker.Deserialize(loadStream);

#### StatusInstance
Remember that you won't normally be serializing StatusInstances directly; that's handled by the StatusTracker.
If you need to serialize a StatusInstance that doesn't currently belong to a StatusTracker, the methods are almost the same:

    statusInstance.Serialize(saveStream);
    // ...and later...
    var loadedInstance = StatusInstance<MyGameObject>.Deserialize(loadStream);


## Advanced example:
Because the value of a StatusInstance can be updated without necessarily knowing which (if any) StatusTracker it belongs to, you might be keeping references to StatusInstances in other parts of your game.

This can be very useful, but creates a challenge for serialization:  When you deserialize a StatusTracker and all its StatusInstances, you'll need to know which StatusInstances are which, so that you can restore those references.

The `statusInstanceCallback` parameter of StatusTracker.Serialize and Deserialize makes this possible. Here's an example of how a game might do this:

    private void SerializeCreature(Creature creature, Stream stream){
        MyJsonHelper.Serialize(creature, stream); // Game-specific logic. Serialize your other data however you like.
        creature.statusTracker.Serialize(stream, SaveStatusInstanceId); // Now every StatusInstance will be saved with the extra data you need.
    }
	private void SaveStatusInstanceId(System.IO.BinaryWriter writer, StatusInstance<Creature> instance, StatusTracker<Creature> tracker){
        int id = MySerializationHelper.GetStatusInstanceId(instance); // Game-specific logic.
        writer.Write(id);
	}

And to deserialize:

    private Creature DeserializeCreature(Stream stream){
        Creature creature = MyJsonHelper.Deserialize<Creature>(stream); // Game-specific logic.
        creature.statusTracker = statusRules.CreateStatusTracker(creature);
        creature.statusTracker.Deserialize(stream, LoadStatusInstanceId);
    }
    private void LoadStatusInstanceId(System.IO.BinaryReader reader, StatusInstance<Creature> instance, Creature creature) {
        int id = reader.ReadInt32();
        MySerializationHelper.SetStatusInstanceId(id, instance); // Game-specific logic to associate id with instance.
    }

By using these callbacks you can store any additional data you need to maintain game state after deserialization.
