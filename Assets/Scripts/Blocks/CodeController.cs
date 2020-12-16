using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeController : MonoBehaviour
{
    public int id;

    List<Block> blocks = new List<Block>();

    // Start is called before the first frame update
    void Start()
    {
        //TEST CASE
        /*StartEvent startEvent = new StartEvent();
        MoveMotion moveMotion = new MoveMotion();
        moveMotion.moveStep = new IntVariable(1);
        //startEvent.AddBlock(moveMotion);

        KeyPressedEvent keyPressedEvent = new KeyPressedEvent();
        keyPressedEvent.key = KeyCode.Space;
        MoveMotion moveMotion2 = new MoveMotion();
        moveMotion2.moveStep = new IntVariable(1);
        //keyPressedEvent.AddBlock(moveMotion2);

        blocks.Add(startEvent);
        blocks.Add(keyPressedEvent);

        startEvent.codeController = this;
        moveMotion.codeController = this;
        keyPressedEvent.codeController = this;
        moveMotion2.codeController = this;*/
    }

    public void LoadCode(CodeData codeData)
    {
        foreach (var blockData in codeData.blocks)
        {
            Block block = Resolve(blockData);
            if (block is Event)
            {
                blocks.Add(block);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AppManager.instance.loaded)
        {
            foreach (var block in blocks)
            {
                if (block is Event)
                {
                    (block as Event).Poll();
                }
            }
        }
    }

    public Block Resolve(BlockData blockData)
    {
        Block result = null;
        if (blockData.blockType == "StartEvent")
        {
            result = gameObject.AddComponent<StartEvent>();
        }
        if (blockData.blockType == "WaitControl")
        {
            result = gameObject.AddComponent<WaitControl>();
        }
        if (blockData.blockType == "MoveMotion")
        {
            result = gameObject.AddComponent<MoveMotion>();
        }
        if (blockData.blockType == "PointsTowardMotion")
        {
            result = gameObject.AddComponent<PointsTowardMotion>();
        }
        if (blockData.blockType == "ShowLook")
        {
            result = gameObject.AddComponent<ShowLook>();
        }
        if (blockData.blockType == "HideLook")
        {
            result = gameObject.AddComponent<HideLook>();
        }
        if (blockData.blockType == "DestroyControl")
        {
            result = gameObject.AddComponent<DestroyControl>();
        }
        if (blockData.blockType == "ReceiveEvent")
        {
            result = gameObject.AddComponent<ReceiveEvent>();
        }
        if (blockData.blockType == "BroadcastEvent")
        {
            result = gameObject.AddComponent<BroadcastEvent>();
        }
        if (blockData.blockType == "InteractEvent")
        {
            result = gameObject.AddComponent<InteractEvent>();
        }
        result.Resolve(blockData);
        result.codeController = this;

        //resolve childs
        Block lastBlock = result;
        foreach (var childBlockData in blockData.blocks)
        {
            Block childBlock = Resolve(childBlockData);
            lastBlock.AddBlock(childBlock);
            lastBlock = childBlock;
        }

        return result;
    }

    public void Interact()
    {
        InteractEvent[] interactEvents = GetComponents<InteractEvent>();
        foreach (var interactEvent in interactEvents)
        {
            interactEvent.Trigger();
        }
    }
}
