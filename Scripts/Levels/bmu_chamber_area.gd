class_name BMUChamberArea
extends Area3D

@export_category("Required Nodes")
@export var camera: Camera3D
@export var player: CharacterBody3D
@export var room_encounter: PackedScene

@export_category("Behavior Variables")
@export var size: Vector3
@export var camera_animation_duration: float = 0.8

@onready var collision_shape: CollisionShape3D = $CollisionShape3D
@onready var camera_marker: Marker3D = $Marker3D
@onready var enemies: Node3D = %Enemies

var player_clamped: bool = false

const CAMERA_DISTANCE_SMOOTHING = 1.35 # NOTE: No tengo idea por que, pero se ve mejor.

func _ready() -> void:
	collision_shape.shape.size = size
	camera_marker.position.y = size.y / 2
	camera_marker.position.z = size.z / 2
	for enemy in enemies.get_children():
		enemy.queue_free()
	var enemies_encounter = room_encounter.instantiate()
	for enemy: Path3D in enemies_encounter.get_children():
		var new_enemy_child: Path3D = enemy.duplicate()
		enemies.add_child(new_enemy_child)
		fix_position(new_enemy_child.position)
		enemy.queue_free()
	enemies.RaiseEnemyCount()

func _physics_process(_delta: float) -> void:
	if player_clamped:
		# NOTE: Quizas deberia estar en el Player Controller.
		player.ClampToCube(global_position, size)
	# DELETE: Sólo para debug. Esto se va.
	if Input.is_action_just_pressed("Attack"):
		_on_room_finished()

func _on_body_entered(body: Node3D) -> void:
	if body == player: # NOTE: no puedo acceder a la class Player desde aca. Deberia ser eso.
		var tween = create_tween()
		tween.set_trans(Tween.TRANS_SINE)
		tween.tween_property(
			camera, "global_position", camera_marker.global_position, camera_animation_duration
		)
		await tween.finished
		# NOTE: Para que se deje de mover.
		camera.reparent(camera_marker, true)
		player_clamped = true

func _on_room_finished() -> void:
	var tween = create_tween()
	tween.set_trans(Tween.TRANS_SINE)
	tween.tween_property(
		camera, "global_position",
		player.position + camera.positionFromTarget / CAMERA_DISTANCE_SMOOTHING,
		camera_animation_duration,
	)
	await tween.finished
	camera.ReparentAndPosition(player)
	player_clamped = false
	# NOTE: Para reducir checks en cada frame, y porque una vez superado la entidad pierde sentido.
	# queue_free()  # NOTE: Cuando se termine de debuggear, descomentar esto.

func fix_position(pos: Vector3):
	if abs(pos.x) > size.x / 2:
		pos.x = randf_range( - size.x / 2, size.x / 2)
	if abs(pos.z) > size.z / 2:
		pos.z = randf_range( - size.z / 2, size.z / 2)
	pos.y = 0
