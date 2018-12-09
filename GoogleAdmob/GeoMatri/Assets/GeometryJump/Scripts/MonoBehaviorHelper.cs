
using UnityEngine;
using System.Collections;

/// <summary>
/// Class to prevent some duplicate code
/// </summary>
namespace NNest.GeometryJump
{
	public class MonoBehaviorHelper : MonoBehaviour 
	{
		private GameManager _gameManager;
		public GameManager gameManager
		{
			get
			{
				if (_gameManager == null)
					_gameManager = FindObjectOfType<GameManager> ();

				return _gameManager;
			}
		}

		private PlayerManager _playerManager;
		public PlayerManager playerManager
		{
			get
			{
				if (_playerManager == null)
					_playerManager = FindObjectOfType<PlayerManager> ();

				return _playerManager;
			}
		}

		private Transform _playerTransform;
		public Transform playerTransform
		{
			get
			{
				if (playerManager == null)
					return null;

				if (_playerTransform == null)
					_playerTransform = playerManager.transform;

				return _playerTransform;
			}
		}

		private SoundManager _soundManager;
		public SoundManager soundManager
		{
			get
			{
				if (_soundManager == null)
					_soundManager = FindObjectOfType<SoundManager> ();

				return _soundManager;
			}
		}

		private Camera _cam;
		public Camera cam
		{
			get
			{
				if (_cam == null)
					_cam = Camera.main;

				return _cam;
			}
		}

		private MainCameraManager _mainCameraManager;
		public MainCameraManager mainCameraManager
		{
			get
			{
				if (_mainCameraManager == null)
					_mainCameraManager = FindObjectOfType<MainCameraManager> ();

				return _mainCameraManager;
			}
		}

		private Transform _camTransform;
		public Transform camTransform
		{
			get
			{
				if (_camTransform == null)
					_camTransform = Camera.main.transform;

				return _camTransform;
			}
		}


		private ColorManager _colorManager;
		public ColorManager colorManager
		{
			get
			{
				if (_colorManager == null)
					_colorManager = FindObjectOfType<ColorManager> ();

				return _colorManager;
			}
		}

		private ContinuousMove _continuousMove;
		public ContinuousMove continuousMove
		{
			get
			{
				if (_continuousMove == null)
					_continuousMove = FindObjectOfType<ContinuousMove> ();

				return _continuousMove;
			}
		}

		private CanvasManager _canvasManager;
		public CanvasManager canvasManager
		{
			get
			{
				if (_canvasManager == null)
					_canvasManager = FindObjectOfType<CanvasManager> ();

				return _canvasManager;
			}
		}

        private DataManager _dataManager;
        public DataManager dataManager
        {
            get
            {
                if (_dataManager == null)
                    _dataManager = FindObjectOfType<DataManager>();

                return _dataManager;
            }
        }
	}
}