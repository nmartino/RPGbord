extends Node3D

@onready var animation_player = $AnimationPlayer
@onready var static_body = $StaticBody
@onready var collision_shape_3d = $StaticBody/CollisionShape3D

func PlayBrokenStone():
	animation_player.play("StoneBreak")
	collision_shape_3d.disabled = true

	
