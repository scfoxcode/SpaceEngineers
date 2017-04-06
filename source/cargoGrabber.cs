// Stephen Surtees Space Engineers Scripting Fun

// Setup guide at
// http://www.spaceengineerswiki.com/Visual_Studio_Setup_Guide

/*---------------------------------------------------------------------------*\
                                NAME GOES HERE

DESCRIPTION:

BLOCKS:
-
-
-
\*---------------------------------------------------------------------------*/ 


#region pre-script
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using VRageMath;
using VRage.Game;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game.EntityComponents;
using VRage.Game.Components;
using VRage.Collections;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
 
namespace IngameScript
{
    public class cargoGrabber : MyGridProgram
    {
#endregion
        //To put your code in a PB copy from this comment...
                // Find working block with name
        public Type FindBlock<Type>(string name) where Type: class, IMyTerminalBlock
        {
            List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.SearchBlocksOfName(name, blocks); 
            for(var i=0; i<blocks.Count; i++)
            {
                if(blocks[i].IsWorking)
                {
                    return blocks[i] as Type;
                }
            } 
            return null;
        }

        public void Main(string containerName)
        {
            IMyCargoContainer cargo = FindBlock<IMyCargoContainer>(containerName);
            IMyTextPanel debug = FindBlock<IMyTextPanel>("debug");
            if(cargo == null || debug == null)
            {
                debug.WritePublicText("Cargo or debug not found!", false);
                return;
            }
            IMyInventory cargoInventory = cargo.GetInventory(0);

            var assemblers = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyAssembler>(assemblers);
            for(int i=0; i<assemblers.Count; i++)
            {
                if(!assemblers[i].HasInventory)
                {
                    continue;
                }
                IMyInventory inventory = assemblers[i].GetInventory(1);
                List<IMyInventoryItem> items = inventory.GetItems();
                debug.WritePublicText("Items Moved:" + items.Count + "\n", true);
                // Move items to cargo
                for(int j=0; j<items.Count; j++)
                {
                    inventory.TransferItemTo(cargoInventory, 0, null, stackIfPossible: true);
                }
            }
        }
        //to this comment.
#region post-script
    }
}
#endregion