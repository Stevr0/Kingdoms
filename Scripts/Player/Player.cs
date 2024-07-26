using UnityEngine;
using Unity.Netcode;

// Enum for player states
public enum PlayerState
{
	Idle,
	Running,
	Jumping,
	Dead,
	Swimming,
	Sleeping,
	Damaged,
	Stunned,
	Building,
	Casting,
	Attacking,
	Climbing,
	Crafting,
	Defending,
	Dodging,
	Healing,
	Interacting,
	SwimmingUnderwater,
	Drowning
}

// Interface for player states
public interface IPlayerState
{
	void Enter(Player player);
	void Execute(Player player);
	void Exit(Player player);
	PlayerState GetStateType(); // To get the type of the state
}

// Implementations of states
public class IdleState : IPlayerState
{
	public void Enter(Player player) { Debug.Log("Entering Idle State"); }
	public void Execute(Player player) { /* Idle logic */ }
	public void Exit(Player player) { Debug.Log("Exiting Idle State"); }
	public PlayerState GetStateType() { return PlayerState.Idle; }
}

public class RunningState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Running State");
		// Set animation, initialize running, etc.
		player.GetComponent<Animator>().Play("Running");
	}

	public void Execute(Player player)
	{
		player.Run(); // Movement logic

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Running State");
		// Clean up running state, if needed
		player.GetComponent<Animator>().Play("Idle");
	}

	public PlayerState GetStateType() { return PlayerState.Running; }
}

public class JumpingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Jumping State");
		// Play jump animation
		player.GetComponent<Animator>().Play("Jumping");
		player.Jump(); // Apply jump logic
	}

	public void Execute(Player player)
	{
		// Logic for maintaining jump state
		if (player.IsGrounded())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Jumping State");
		// Reset jump state if needed
	}

	public PlayerState GetStateType() { return PlayerState.Jumping; }
}

public class DeadState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Dead State");
		// Disable controls and play death animation
		player.GetComponent<Animator>().Play("Dead");
		player.enabled = false;
	}

	public void Execute(Player player)
	{
		// Handle logic for a dead player
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Dead State");
		// Clean up death state
	}

	public PlayerState GetStateType() { return PlayerState.Dead; }
}

public class SwimmingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Swimming State");
		// Set swimming animation and physics adjustments
		player.GetComponent<Animator>().Play("Swimming");
		player.AdjustPhysicsForSwimming();
	}

	public void Execute(Player player)
	{
		player.Swim(); // Swimming logic

		if (!player.IsSwimmingOnSurface())
		{
			player.ChangeState(new DrowningState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Swimming State");
		// Reset swimming physics
		player.ResetPhysics();
	}

	public PlayerState GetStateType() { return PlayerState.Swimming; }
}

public class SleepingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Sleeping State");
		player.GetComponent<Animator>().Play("Sleeping");
		player.enabled = false; // Disable controls
	}

	public void Execute(Player player)
	{
		// Handle sleeping logic
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Sleeping State");
		player.enabled = true; // Re-enable controls
	}

	public PlayerState GetStateType() { return PlayerState.Sleeping; }
}

public class DamagedState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Damaged State");
		player.GetComponent<Animator>().Play("Damaged");
		player.ApplyDamageEffects();
	}

	public void Execute(Player player)
	{
		// Handle damage effects
		if (player.HealingComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Damaged State");
		// Clean up damage effects
	}

	public PlayerState GetStateType() { return PlayerState.Damaged; }
}

public class StunnedState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Stunned State");
		player.GetComponent<Animator>().Play("Stunned");
		player.enabled = false;
	}

	public void Execute(Player player)
	{
		// Handle stunned logic
		if (player.RecoveredFromStun())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Stunned State");
		player.enabled = true;
	}

	public PlayerState GetStateType() { return PlayerState.Stunned; }
}

public class BuildingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Building State");
		player.GetComponent<Animator>().Play("Building");
	}

	public void Execute(Player player)
	{
		// Building logic
		if (player.BuildingComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Building State");
	}

	public PlayerState GetStateType() { return PlayerState.Building; }
}

public class CastingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Casting State");
		player.GetComponent<Animator>().Play("Casting");
	}

	public void Execute(Player player)
	{
		// Casting logic
		if (player.CastingComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Casting State");
	}

	public PlayerState GetStateType() { return PlayerState.Casting; }
}

public class AttackingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Attacking State");
		player.GetComponent<Animator>().Play("Attacking");
	}

	public void Execute(Player player)
	{
		// Attacking logic
		if (player.AttackComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Attacking State");
	}

	public PlayerState GetStateType() { return PlayerState.Attacking; }
}

public class ClimbingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Climbing State");
		player.GetComponent<Animator>().Play("Climbing");
	}

	public void Execute(Player player)
	{
		// Climbing logic
		if (player.ClimbingComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Climbing State");
	}

	public PlayerState GetStateType() { return PlayerState.Climbing; }
}

public class CraftingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Crafting State");
		player.GetComponent<Animator>().Play("Crafting");
	}

	public void Execute(Player player)
	{
		// Crafting logic
		if (player.CraftingComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Crafting State");
	}

	public PlayerState GetStateType() { return PlayerState.Crafting; }
}

public class DefendingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Defending State");
		player.GetComponent<Animator>().Play("Defending");
	}

	public void Execute(Player player)
	{
		// Defending logic
		if (player.DefendingComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Defending State");
	}

	public PlayerState GetStateType() { return PlayerState.Defending; }
}

public class DodgingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Dodging State");
		player.GetComponent<Animator>().Play("Dodging");
	}

	public void Execute(Player player)
	{
		// Dodging logic
		if (player.DodgingComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Dodging State");
	}

	public PlayerState GetStateType() { return PlayerState.Dodging; }
}

public class HealingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Healing State");
		player.GetComponent<Animator>().Play("Healing");
	}

	public void Execute(Player player)
	{
		// Healing logic
		if (player.HealingComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Healing State");
	}

	public PlayerState GetStateType() { return PlayerState.Healing; }
}

public class InteractingState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering Interacting State");
		player.GetComponent<Animator>().Play("Interacting");
	}

	public void Execute(Player player)
	{
		// Interacting logic
		if (player.InteractionComplete())
		{
			player.ChangeState(new IdleState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Interacting State");
	}

	public PlayerState GetStateType() { return PlayerState.Interacting; }
}

public class SwimmingUnderwaterState : IPlayerState
{
	public void Enter(Player player)
	{
		Debug.Log("Entering SwimmingUnderwater State");
		player.GetComponent<Animator>().Play("SwimmingUnderwater");
		player.AdjustPhysicsForUnderwater();
	}

	public void Execute(Player player)
	{
		// Swimming underwater logic
		if (player.IsOnSurface())
		{
			player.ChangeState(new SwimmingState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting SwimmingUnderwater State");
		player.ResetPhysics();
	}

	public PlayerState GetStateType() { return PlayerState.SwimmingUnderwater; }
}

public class DrowningState : IPlayerState
{
	private Animator animator;
	private float drowningTime = 10f;
	private float damageInterval = 1f;
	private float damageTimer;
	private float drowningTimer;
	private bool isDrowning = false;

	public void Enter(Player player)
	{
		Debug.Log("Entering Drowning State");
		animator = player.GetComponent<Animator>();
		drowningTimer = drowningTime;
		damageTimer = damageInterval;

		animator.Play("Drowning");
		player.enabled = false;
		isDrowning = true;
	}

	public void Execute(Player player)
	{
		if (isDrowning)
		{
			drowningTimer -= Time.deltaTime;
			damageTimer -= Time.deltaTime;

			if (drowningTimer <= 0f)
			{
				player.TakeDamage();
				player.HandleDeath();
				player.ChangeState(new DeadState());
				isDrowning = false;
			}
			else if (damageTimer <= 0f)
			{
				player.TakeDamage();
				damageTimer = damageInterval;
			}
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Drowning State");
		player.enabled = true;
		animator.Play("Idle");
	}

	public PlayerState GetStateType() { return PlayerState.Drowning; }
}

// Player class with all state integrations
public class Player : NetworkBehaviour
{
	private IPlayerState currentState;

	// Network variables for synchronizing state
	public NetworkVariable<PlayerState> CurrentState = new NetworkVariable<PlayerState>(PlayerState.Idle);

	public override void OnNetworkSpawn()
	{
		if (IsLocalPlayer)
		{
			// Set initial state for local player
			ChangeState(new IdleState());
		}
		else
		{
			// For non-local players, set up client-side state handling
			CurrentState.OnValueChanged += HandleStateChange;
		}
	}

	void Update()
	{
		if (IsLocalPlayer)
		{
			// Execute the current state logic for local player
			currentState?.Execute(this);

			// Update network variable for state synchronization
			UpdatePlayerStateServerRpc(currentState.GetStateType());
		}
	}

	public void ChangeState(IPlayerState newState)
	{
		if (!IsLocalPlayer) return;

		// Exit the current state
		currentState?.Exit(this);

		// Change to the new state
		currentState = newState;

		// Enter the new state
		currentState.Enter(this);

		// Update network variable
		CurrentState.Value = newState.GetStateType();
	}

	// Networked method for state synchronization
	[ServerRpc]
	private void UpdatePlayerStateServerRpc(PlayerState state)
	{
		CurrentState.Value = state;
	}

	private void HandleStateChange(PlayerState oldState, PlayerState newState)
	{
		// Update the state for non-local players
		currentState?.Exit(this);
		currentState = GetStateFromType(newState);
		currentState.Enter(this);
	}

	private IPlayerState GetStateFromType(PlayerState state)
	{
		return state switch
		{
			PlayerState.Idle => new IdleState(),
			PlayerState.Running => new RunningState(),
			PlayerState.Jumping => new JumpingState(),
			PlayerState.Dead => new DeadState(),
			PlayerState.Swimming => new SwimmingState(),
			PlayerState.Sleeping => new SleepingState(),
			PlayerState.Damaged => new DamagedState(),
			PlayerState.Stunned => new StunnedState(),
			PlayerState.Building => new BuildingState(),
			PlayerState.Casting => new CastingState(),
			PlayerState.Attacking => new AttackingState(),
			PlayerState.Climbing => new ClimbingState(),
			PlayerState.Crafting => new CraftingState(),
			PlayerState.Defending => new DefendingState(),
			PlayerState.Dodging => new DodgingState(),
			PlayerState.Healing => new HealingState(),
			PlayerState.Interacting => new InteractingState(),
			PlayerState.SwimmingUnderwater => new SwimmingUnderwaterState(),
			PlayerState.Drowning => new DrowningState(),
			_ => new IdleState(),
		};
	}

	// Methods for player actions
	[ServerRpc]
	public void MoveServerRpc(Vector3 direction)
	{
		Move(direction);
		UpdateClientMovementClientRpc(direction);
	}

	[ClientRpc]
	private void UpdateClientMovementClientRpc(Vector3 direction)
	{
		if (!IsLocalPlayer)
		{
			Move(direction);
		}
	}

	public void Move(Vector3 direction) { /* Movement logic */ }
	public void Attack() { /* Attack logic */ }
	public void Jump() { /* Jump logic */ }
	public void Defend() { /* Defend logic */ }
	public void Interact() { /* Interaction logic */ }
	public void Heal() { /* Healing logic */ }
	public void Craft() { /* Crafting logic */ }
	public void Build() { /* Building logic */ }
	public void Dodge() { /* Dodging logic */ }
	public void Stunned() { /* Stunned logic */ }
	public void Cast() { /* Casting logic */ }
	public void Swim() { /* Swimming logic */ }
	public void SwimUnderwater() { /* Underwater swimming logic */ }
	public void Climb() { /* Climbing logic */ }
	public void TakeDamage() { /* Damage logic */ }
	public void HandleDeath() { /* Death logic */ }

	// State conditions
	public bool IsGrounded() { return true; } // Placeholder
	public bool IsOnLand() { return true; } // Placeholder
	public bool IsAwake() { return true; } // Placeholder
	public bool InteractionComplete() { return true; } // Placeholder
	public bool HealingComplete() { return true; } // Placeholder
	public bool CraftingComplete() { return true; } // Placeholder
	public bool BuildingComplete() { return true; } // Placeholder
	public bool RecoveredFromStun() { return true; } // Placeholder
	public bool IsSwimmingOnSurface() { return true; } // Placeholder
	public bool IsDead() { return false; } // Placeholder
	public bool IsOnSurface() { return true; } // Placeholder for checking if on the surface
	public bool CastingComplete() { return true; } // Placeholder
}

