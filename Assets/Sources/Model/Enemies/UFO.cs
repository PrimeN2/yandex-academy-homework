using UnityEngine;

namespace Asteroids.Model
{
    public class UFO : Enemy
    {
        public readonly int Team;

        private readonly UFOCollisionHandler _collisionHandler;

		private readonly float _speed;

		private readonly Transformable _player;
		private Transformable _target;

        private int _enemyTeam { get => Team == 0 ? 1 : 0; }

		public UFO(UFOCollisionHandler handler, Transformable target, 
            Vector2 position, float speed, int team) : base(position, 0)
        {
            _collisionHandler = handler;

			_player = target;
			_target = target;
			_speed = speed;
            Team = team;
        }

        public override void Update(float deltaTime)
        {
            Vector2 nextPosition = Vector2.MoveTowards(Position, _target.Position, _speed * deltaTime);
            MoveTo(nextPosition);
            UpdateTarget();
            LookAt(_target.Position);
        }

		public void Collide(UFO enemy)
        {
			_collisionHandler.Handle(this, enemy);
        }

		private void UpdateTarget()
		{
            _target = GlobalTeamsLists.TotalAmount > 1 ? GlobalTeamsLists.Teams[_enemyTeam][0] : _player;
		}

		private void LookAt(Vector2 point)
        {
            Rotate(Vector2.SignedAngle(Quaternion.Euler(0, 0, Rotation) * Vector3.up, (Position - point)));
        }
    }
}
