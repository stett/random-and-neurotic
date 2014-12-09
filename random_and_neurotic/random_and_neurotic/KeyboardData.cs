using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace random_and_neurotic
{
    public class KeyboardData
    {
        public enum KeyState { Up, Released, Pressed, Down };
        Dictionary<Keys, KeyState> key_map = new Dictionary<Keys, KeyState>();

        public void update()
        {
            Dictionary<Keys, KeyState> key_changes = new Dictionary<Keys, KeyState>();
            foreach (KeyValuePair<Keys, KeyState> kv in key_map)
                if (key_map[kv.Key] == KeyState.Released) key_changes[kv.Key] = KeyState.Up;
                else if (Keyboard.GetState().IsKeyUp(kv.Key)) key_changes[kv.Key] = KeyState.Released;

            foreach (KeyValuePair<Keys, KeyState> kv in key_changes)
                if (key_changes[kv.Key] == KeyState.Up) key_map.Remove(kv.Key);
                else key_map[kv.Key] = key_changes[kv.Key];

            foreach (Keys key in Keyboard.GetState().GetPressedKeys())
                if (key_map.ContainsKey(key)) key_map[key] = KeyState.Down;
                else key_map[key] = KeyState.Pressed;
        }

        public KeyState key_state(Keys key)
        {
            if (key_map.ContainsKey(key)) return key_map[key];
            else return KeyState.Up;
        }

        public bool key_pressed(Keys key) { return key_state(key) == KeyState.Pressed; }
        public bool key_released(Keys key) { return key_state(key) == KeyState.Released; }
        public bool key_down(Keys key) { return key_pressed(key) || key_state(key) == KeyState.Down; }
        public bool key_up(Keys key) { return key_released(key) || key_state(key) == KeyState.Up; }
    }
}
