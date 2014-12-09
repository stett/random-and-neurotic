using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace random_and_neurotic
{
    public class Neuron
    {
        protected float charge = 0;
        float prev_charge = 0;
        List<Neuron> connections = new List<Neuron>();
        Vector2 position = new Vector2();
        Network network;

        public Neuron(Network network, Vector2 position)
        {
            this.network = network;
            this.position = position;
            this.charge = 0;
        }

        public void connect()
        {
            int num_connections = (int)Math.Max(1, Settings.MAX_NEURON_CONNECTIONS * Utils.rand());
            while (connections.Count < num_connections)
            {
                float r_angle = 2 * (float)Math.PI * Utils.rand();
                float r_rad = Settings.MAX_NEURON_CONNECTION_RADIUS * Utils.rand();
                Vector2 r_pos = r_rad * new Vector2((float)Math.Cos(r_angle), (float)Math.Sin(r_angle));
                Vector2 n_pos = position + r_pos;
                int x = (int)Math.Round(n_pos.X);
                int y = (int)Math.Round(n_pos.Y);

                while (x < 0) { x += network.w; }
                while (y < 0) { y += network.h; }
                while (x > network.w - 1) { x -= network.w; }
                while (y > network.h - 1) { y -= network.h; }

                if (x > 0 && x < network.w && y > 0 && y < network.h)
                {
                    Neuron neuron = network.neurons[x, y];
                    if (neuron != this && !connections.Contains(neuron))
                    {
                        if (Utils.rand() < Settings.MAX_NEURON_CONNECTIONS / (4 * Settings.MAX_NEURON_CONNECTION_RADIUS * Settings.MAX_NEURON_CONNECTION_RADIUS))
                            connections.Add(neuron);
                    }
                }
            }
        }
        
        public void update(int pass = 0)
        {
            if (pass == 0)
            {
                prev_charge = charge;
                charge = 0;
            }
            else if (pass == 1)
            {
                float charge_fraction = prev_charge / connections.Count;
                foreach (Neuron connection in connections)
                    connection.charge += charge_fraction;
            }
        }

        public void set_charge(float charge)
        {
            this.charge = charge;// Math.Min(1, Math.Max(0, charge));
        }
        public float get_charge() { return charge; }
        public Vector2 get_position() { return position; }
    }
}
