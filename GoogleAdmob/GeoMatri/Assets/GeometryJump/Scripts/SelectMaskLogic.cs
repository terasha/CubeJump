/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
#if AADOTWEEN
using DG.Tweening;
#endif

/// <summary>
/// Class in charge of the logic behind the selection of a new mask in the character shop
/// </summary>
namespace NNest.GeometryJump
{
	public class SelectMaskLogic : MonoBehaviour 
	{
		public Button buttonNext;
		public Button buttonPrevious;

        public Camera CharacterCam;
        public Transform CharacterList;
		public List<RectTransform> listMask = new List<RectTransform>();
        public List<Transform> ListCharacter = new List<Transform>();

		public Vector2 scaleNextAndPrevious = Vector2.one;

		public float posPreviousNext = 2;

		public int position = 0;

		public Transform previousPrevious;
		public Transform previous;
		public Transform current;
		public Transform next;
		public Transform nextNext;


        private GameObject PreLoad;

		void Awake()
		{
            CharacterCam.gameObject.SetActive(false);
            int count = CharacterList.GetChildCount();
            for (int i = 0; i < count; i++)
            {
                ListCharacter.Add(CharacterList.GetChild(i));
            }
        }

		void Start()
		{
            SetPreviousPrevious();
            SetPrevious();
            SetCurrent();
            SetNext();
            SetNextNext();
            previousPrevious.localPosition = new Vector3(-2 * posPreviousNext,0,0);
            previous.localPosition = new Vector3(-posPreviousNext, 0 , 0);
            current.localPosition = Vector3.zero;
            next.localPosition = new Vector3(posPreviousNext, 0,0);
            nextNext.localPosition = new Vector3(2 * posPreviousNext, 0,0);

            //previousPrevious.localScale = Vector2.one *0.5f;
            //previous.localScale = scaleNextAndPrevious;
            //current.localScale = Vector2.one * 2f;
            //next.localScale = scaleNextAndPrevious;
            //nextNext.localScale = Vector2.one * 0.5f;
        }

		void OnEnable()
		{
            CharacterCam.gameObject.SetActive(true);
            buttonNext.onClick.AddListener(OnClickedNext);
			buttonPrevious.onClick.AddListener(OnClickedPrevious);
		}

		void OnDisable()
		{
            CharacterCam.gameObject.SetActive(false);
            buttonNext.onClick.RemoveAllListeners();
			buttonPrevious.onClick.RemoveAllListeners();
		}


		void SetIcons()
		{


			foreach(var r in listMask)
			{
				r.gameObject.SetActive(false);
			}

			SetPreviousPrevious();
			SetPrevious();
			SetCurrent();
			SetNext();
			SetNextNext();

			foreach(var r in listMask)
			{
				if(r != previousPrevious && r != previous && r != current && r != next && r != nextNext ) 
					r.gameObject.SetActive(false);
			}
		}
		void SetPreviousPrevious()
		{
			var r = GetPosition(position - 2);
			previousPrevious = r;
		}

		void SetPrevious()
		{
			var r = GetPosition(position - 1);
			previous = r;
		}

		void SetCurrent()
		{
			var r = GetPosition(position);
			current = r;
		}

		void SetNext()
		{
			var r = GetPosition(position + 1);
			next = r;
		}

		void SetNextNext()
		{
			var r = GetPosition(position + 2);
			nextNext = r;
		}


		Transform GetPosition(int p)
		{
			if(p < 0)
				p = p + (1 - p/ ListCharacter.Count) * ListCharacter.Count;

			if(p >= listMask.Count)
				p = p - (p/ ListCharacter.Count) * ListCharacter.Count;

			return ListCharacter[p];
		}

		public void OnClickedNext()
		{

			DesactiveButton();

			Invoke("ReactiveButton",0.2f);

			//DoScale(previousPrevious, 0.5f, scaleNextAndPrevious.x);
			DoPosition(previousPrevious ,-2*posPreviousNext, -posPreviousNext);

			DoScale(previous, scaleNextAndPrevious.x, 1f);
			DoPosition(previous ,-posPreviousNext, 0);


			DoScale(current, 2f, scaleNextAndPrevious.x);
			DoPosition(current, 0, posPreviousNext);


			DoScale(next,scaleNextAndPrevious.x, 0);
			DoPosition(next, posPreviousNext, 2*posPreviousNext);

            DoPosition(nextNext, -3 * posPreviousNext, -2 * posPreviousNext);
            position --;

			SetPreviousPrevious();
			SetPrevious();
			SetCurrent();
			SetNext();
			SetNextNext();

		} 

		public void OnClickedPrevious()
		{

			DesactiveButton();

			Invoke("ReactiveButton",0.2f);


            DoPosition(previousPrevious, 3 * posPreviousNext, 2 * posPreviousNext);

            DoScale(previous, scaleNextAndPrevious.x, 0);
			DoPosition(previous ,-posPreviousNext, -2*posPreviousNext);

			DoScale(current, 2f, scaleNextAndPrevious.x);
			DoPosition(current, 0, -posPreviousNext);


			DoScale(next,scaleNextAndPrevious.x, 1);
			DoPosition(next, posPreviousNext, 0);

			DoScale(nextNext, 0.5f, scaleNextAndPrevious.x);
			DoPosition(nextNext ,+2*posPreviousNext, posPreviousNext);

            
            position ++;

			SetPreviousPrevious();
			SetPrevious();
			SetCurrent();
			SetNext();
			SetNextNext();
		}

		void DesactiveButton()
		{
			buttonNext.enabled = false;
			buttonPrevious.enabled = false;
		}

		void ReactiveButton()
		{
			buttonNext.enabled = true;
			buttonPrevious.enabled = true;
		}

		void DoScale(Transform r, float fromS, float toS)
		{
            return;
			DoScale(r,fromS,toS,0.3f);
		}

		void DoScale(Transform r, float fromS, float toS, float duration)
		{
			r.localScale = Vector2.one * fromS;

			#if AADOTWEEN
			r.DOScale(toS, duration);
			#endif
		}

		void DoPosition(Transform r, float fromS, float toS)
		{
			DoPosition(r,fromS,toS,0.3f);
		}

		void DoPosition(Transform r, float fromS, float toS, float duration)
		{
			r.localPosition = new Vector3(fromS, 0,0);

			#if AADOTWEEN
            r.DOMoveX(toS, duration);
			#endif
		}
	}
}