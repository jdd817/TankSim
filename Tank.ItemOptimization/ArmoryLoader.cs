using BattleNetApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.ItemOptimization.BattleNetAdapters;

namespace Tank.ItemOptimization
{
    public class ArmoryLoader
    {
        private IBattleNetClient _client;
        private IAdapter<BattleNetApi.DataObjects.Item, Item> _itemAdapter;

        public ArmoryLoader(
            IBattleNetClient client,
            IAdapter<BattleNetApi.DataObjects.Item, Item> itemAdapter)
        {
            _client = client;
            _itemAdapter = itemAdapter;
        }

        public Character LoadCharacter(string name, string server)
        {
            var bnetChar = _client.Character.Items(server, name);
            var character = new Character { EquippedItems = new Dictionary<Slot, Item>(), Inventory = new List<Item>() };

            if (bnetChar.items.head != null)
                character.EquippedItems.Add(Slot.Head, _itemAdapter.Convert(bnetChar.items.head).Set(x=>x.Slot=ItemType.Head));
            if (bnetChar.items.neck != null)
                character.EquippedItems.Add(Slot.Neck, _itemAdapter.Convert(bnetChar.items.neck).Set(x => x.Slot = ItemType.Neck));
            if (bnetChar.items.shoulder != null)
                character.EquippedItems.Add(Slot.Shoulder, _itemAdapter.Convert(bnetChar.items.shoulder).Set(x => x.Slot = ItemType.Shoulder));
            if (bnetChar.items.back != null)
                character.EquippedItems.Add(Slot.Back, _itemAdapter.Convert(bnetChar.items.back).Set(x => x.Slot = ItemType.Back));
            if (bnetChar.items.chest != null)
                character.EquippedItems.Add(Slot.Chest, _itemAdapter.Convert(bnetChar.items.chest).Set(x => x.Slot = ItemType.Chest));
            if (bnetChar.items.wrist != null)
                character.EquippedItems.Add(Slot.Wrist, _itemAdapter.Convert(bnetChar.items.wrist).Set(x => x.Slot = ItemType.Wrist));
            if (bnetChar.items.hands != null)
                character.EquippedItems.Add(Slot.Hands, _itemAdapter.Convert(bnetChar.items.hands).Set(x => x.Slot = ItemType.Hands));
            if (bnetChar.items.waist != null)
                character.EquippedItems.Add(Slot.Waist, _itemAdapter.Convert(bnetChar.items.waist).Set(x => x.Slot = ItemType.Waist));
            if (bnetChar.items.legs != null)
                character.EquippedItems.Add(Slot.Legs, _itemAdapter.Convert(bnetChar.items.legs).Set(x => x.Slot = ItemType.Legs));
            if (bnetChar.items.feet != null)
                character.EquippedItems.Add(Slot.Feet, _itemAdapter.Convert(bnetChar.items.feet).Set(x => x.Slot = ItemType.Feet));
            if (bnetChar.items.finger1 != null)
                character.EquippedItems.Add(Slot.Finger1, _itemAdapter.Convert(bnetChar.items.finger1).Set(x => x.Slot = ItemType.Finger));
            if (bnetChar.items.finger2 != null)
                character.EquippedItems.Add(Slot.Finger2, _itemAdapter.Convert(bnetChar.items.finger2).Set(x => x.Slot = ItemType.Finger));
            if (bnetChar.items.trinket1 != null)
                character.EquippedItems.Add(Slot.Trinket1, _itemAdapter.Convert(bnetChar.items.trinket1).Set(x => x.Slot = ItemType.Trinket));
            if (bnetChar.items.trinket2 != null)
                character.EquippedItems.Add(Slot.Trinket2, _itemAdapter.Convert(bnetChar.items.trinket2).Set(x => x.Slot = ItemType.Trinket));
            if (bnetChar.items.mainHand != null)
                character.EquippedItems.Add(Slot.MainHand, _itemAdapter.Convert(bnetChar.items.mainHand).Set(x => x.Slot = ItemType.Weapon));
            if (bnetChar.items.offHand != null)
                character.EquippedItems.Add(Slot.OffHand, _itemAdapter.Convert(bnetChar.items.offHand).Set(x => x.Slot = ItemType.Weapon));

            return character;
        }
    }
}
