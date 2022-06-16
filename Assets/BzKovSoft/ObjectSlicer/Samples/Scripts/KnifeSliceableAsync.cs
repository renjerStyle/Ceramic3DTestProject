﻿using System.Collections;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer.Samples
{
	/// <summary>
	/// This script will invoke slice method of IBzSliceableNoRepeat interface if knife slices this GameObject.
	/// The script must be attached to a GameObject that have rigidbody on it and
	/// IBzSliceable implementation in one of its parent.
	/// </summary>
	[DisallowMultipleComponent]
	public class KnifeSliceableAsync : MonoBehaviour
	{
		IBzSliceableNoRepeat _sliceableAsync;

		void Start()
		{
			_sliceableAsync = GetComponentInParent<IBzSliceableNoRepeat>();
		}

        private void OnTriggerEnter(Collider other)
        {
			var knife = other.gameObject.GetComponent<BzKnife>();
			/*if (knife == null)
				return;*/
			if (knife != null)
			{
				//StartCoroutine(Slice(knife));
				SliceMain(knife);
			}
		}

        private void OnTriggerExit(Collider other)
        {
			var knife = other.gameObject.GetComponent<BzKnife>();
			/*if (knife == null)
				return;*/
			if (knife != null)
			{
				//StartCoroutine(Slice(knife));
				SliceMain(knife);
			}
		}

        void OnTriggerStay(Collider other)
		{
			var knife = other.gameObject.GetComponent<BzKnife>();
			/*if (knife == null)
				return;*/
			if (knife != null)
			{
				//StartCoroutine(Slice(knife));
				SliceMain(knife);
			}
		}

		private void SliceMain(BzKnife knife)
		{
			// The call from OnTriggerEnter, so some object positions are wrong.
			// We have to wait for next frame to work with correct values

			//Vector3 point = GetCollisionPoint(knife);
			//Vector3 normal = Vector3.Cross(knife.MoveDirection, knife.BladeDirection);
			Vector3 point = knife.transform.position;
			point.x += 0.26f;
			point.y += 0.26f;
			point.z += 0.26f;
			Vector3 normal = knife.transform.forward;
			Plane plane = new Plane(normal, point);

			if (_sliceableAsync != null)
			{
				_sliceableAsync.Slice(plane, knife.SliceID, null);
			}
		}

		private IEnumerator Slice(BzKnife knife)
		{
			// The call from OnTriggerEnter, so some object positions are wrong.
			// We have to wait for next frame to work with correct values

			//Vector3 point = GetCollisionPoint(knife);
			//Vector3 normal = Vector3.Cross(knife.MoveDirection, knife.BladeDirection);
			Vector3 point = knife.transform.position;
			point.x += 0.26f;
			point.y += 0.26f;
			point.z += 0.26f;
			Vector3 normal = knife.transform.forward;
			Plane plane = new Plane(normal, point);

			if (_sliceableAsync != null)
			{
				_sliceableAsync.Slice(plane, knife.SliceID, null);
			}
			yield return null;
		}

		private Vector3 GetCollisionPoint(BzKnife knife)
		{
			Vector3 distToObject = transform.position - knife.Origin;
			Vector3 proj = Vector3.Project(distToObject, knife.BladeDirection);

			Vector3 collisionPoint = knife.Origin + proj;
			return collisionPoint;
		}
	}
}