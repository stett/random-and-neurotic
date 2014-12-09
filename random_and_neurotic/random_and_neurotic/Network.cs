using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace random_and_neurotic
{
    public class Network
    {
        public int w;
        public int h;
        public Neuron[,] neurons;

        public Network(int w, int h)
        {
            this.w = w;
            this.h = h;
            neurons = new Neuron[w, h];
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                neurons[x,y] = new Neuron(this, new Vector2(x, y));
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                neurons[x, y].connect();
        }

        public void update()
        {
            foreach (Neuron neuron in neurons) neuron.update(0);
            foreach (Neuron neuron in neurons) neuron.update(1);
        }
    }
}
