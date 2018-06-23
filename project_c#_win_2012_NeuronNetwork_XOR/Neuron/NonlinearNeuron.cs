using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron
{
    public class NonlinearNeuron: Neuron {

	public NonlinearNeuron() {
	}

	public NonlinearNeuron(List<Double> weights) {
		this.setWeights(weights);
	}

	protected override double transferFunction(double excitement) {
		return 1 / (1 + Math.Exp(-2 * excitement));
	}

    public override double getWeightUpdate(int weightIndex, double delta, List<Double> signal)
    {
		if (weightIndex == 0) {
			return LEARNING_RATE * delta * THRESHOLD_SIGNAL * propagate(signal)
					* (1 - propagate(signal));
		}

        return LEARNING_RATE * delta * signal[weightIndex-1] * propagate(signal) * (1 - propagate(signal));
	}
}
}
