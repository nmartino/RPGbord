class_name BMUChamberArea
extends Area3D

@export_category("Required Nodes")
@export var camera: Camera3D
@export var player: CharacterBody3D
@export var enemies: Array[Path3D]
@export var stone: Node3D

@export_category("Behavior Variables")
@export var size: Vector3
@export var camera_animation_duration: float = 0.8
## Poner enemigos del cuarto en cuestion solamente.

@onready var collision_shape: CollisionShape3D = $CollisionShape3D
@onready var camera_marker: Marker3D = $Marker3D

var player_clamped: bool = false
var adjusting: bool = false

func _ready() -> void:
	collision_shape.shape.size = size
	camera_marker.position.y = size.y / 2
	camera_marker.position.z = size.z / 2
	# NOTE: No quiero cuartos sin enemies.
	if enemies.size() == 0:
		queue_free()

func _physics_process(_delta: float) -> void:
	if player_clamped:
			# NOTE: Se puede resolver con una StaticBody invisible, quizas.
		player.ClampToCube(global_position, size)

func _process(_delta: float) -> void:
	if enemies.size() == 0 and not adjusting:
		adjusting = true
		await _on_room_finished()
		adjusting = false
	cleanup_enemies_array()

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
	stone.PlayBrokenStone()
	camera.reparent(player, true)
	tween.tween_property(
		camera, "position",
		camera.positionFromTarget,
		camera_animation_duration,
	)
	await tween.finished
	camera.ReparentAndPosition(player)
	player_clamped = false
	# NOTE: Para reducir checks en cada frame, y porque una vez superado la entidad pierde sentido.
	queue_free()

func fix_position(pos: Vector3):
	if abs(pos.x) > size.x / 2:
		pos.x = randf_range( - size.x / 2, size.x / 2)
	if abs(pos.z) > size.z / 2:
		pos.z = randf_range( - size.z / 2, size.z / 2)
	pos.y = 0

func cleanup_enemies_array() -> void:
	var cleanup_array: Array[Path3D] = []
	for enemy: Path3D in enemies:
		if is_instance_valid(enemy):
			cleanup_array.push_back(enemy)
	enemies = cleanup_array
