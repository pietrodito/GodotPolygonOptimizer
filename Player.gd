extends KinematicBody2D

export var max_speed := 50
export var gravity := 10 
var _gravity_direction := Vector2.DOWN

var _velocity := Vector2.ZERO

var _direction := Vector2.ZERO

func _physics_process(delta: float) -> void:
	__get_direction()
	__apply_gravity(delta)
	__compute_velocity()
	__move()

func __get_direction() -> void:
	_direction.x = Input.is_key_pressed(KEY_RIGHT) as int - Input.is_key_pressed(KEY_LEFT) as int


func __apply_gravity(delta: float) -> void:
	_velocity.y += _gravity_direction.y * gravity * delta

func __compute_velocity() -> void:
	_velocity.x = max_speed * _direction.x

func __move() -> void:
	var collision = move_and_collide(_velocity)
	if collision:
		rotation = collision.normal.angle()
		_velocity = _velocity.slide(collision.normal)
