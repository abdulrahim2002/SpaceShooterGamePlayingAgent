# Game Playing Agent

- The agent is trained using [_proximal policy optimization_](https://arxiv.org/abs/1707.06347).
- The following is the result after 500,000 training steps.

## Performance

https://github.com/abdulrahim2002/SpaceShooterGamePlayingAgent/assets/89011337/924601a1-162e-402b-8f9e-422cb5c2a164

---

https://github.com/abdulrahim2002/SpaceShooterGamePlayingAgent/assets/89011337/f8fbcf57-99a7-4d0e-a6d3-e0594423b969


## TODO

* Implement soft-actor-critic(SAC).
* Emperically optimize, neural network parameters: depth, input layer
* Hyperparameter-tuning.

### Reward Function 

The reward funciton is:
```
**timeSurvived + 3 * rocksDestroyed + 5 * enemyShipsDestroyed**
```

Rational:
the agent would be incentivised to shoot down enemy bullets and rocks

Input:
The observations are: positions of objects currently in the scene, feeded sequentially
with a label after each position to differentiate between objects

Parameters Used:
* Input layer neurons: 30
* Depth: 2

### Shortcomings
* Limited Computational Power
