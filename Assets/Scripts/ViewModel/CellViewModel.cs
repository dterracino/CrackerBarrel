﻿using UnityEngine;
using System.Collections;
using System.Linq;
using Foundation.Databinding;
using System;
using DG.Tweening;

namespace CrackerBarrel
{
    public class CellViewModel : ObservableBehaviour
    {
        public GameObject Peg;

        private Cell _cell;
        public Cell Cell { 
            get { return _cell; }
            set {
                if (value == _cell)
                    return;

                _cell = value;
                RaiseBindingUpdate(nameof(Cell), _cell);
            }
        }

        private bool _isHighlighted;
        public bool IsHighlighted {
            get { return _isHighlighted; }
            set {
                if (value == _isHighlighted)
                    return;
                _isHighlighted = value;
                RaiseBindingUpdate(nameof(IsHighlighted), _isHighlighted);
            }
        }

        private bool _isSelected;
        public bool IsSelected {
            get { return _isSelected; }
            private set {
                if (value == _isSelected)
                    return;
                _isSelected = value;
                RaiseBindingUpdate(nameof(IsSelected), _isSelected);
            }
        }

        private Vector3 originalLocalPosition;
        private Vector3 originalLocalScale;

        public void SelectCell(Vector3 holdWorldPosition)
        {
            // bail out to prevent animation from running again
            if (IsSelected)
                return;

            IsSelected = true;

            // trigger animation. There are better ways to do this, but this is good enough for now.
            originalLocalPosition = Peg.transform.localPosition;
            originalLocalScale = Peg.transform.localScale;
            var t = Peg.transform;
            DOTween.Sequence()
                .Append(t.DOBlendableScaleBy(0.5f * Vector3.one, 0.2f)) // 2D represenation of peg coming out of board
                .Append(t.DOMove(holdWorldPosition, 0.3f));
        }

        public void DeselectCell()
        {
            // bail out to prevent animation from running again
            if (!IsSelected)
                return;

            IsSelected = false;

            // trigger animation. There are better ways to do this, but this is good enough for now.
            var t = Peg.transform;
            DOTween.Sequence()
                .Append(t.DOLocalMove(originalLocalPosition, 0.3f))
                .Append(t.DOScale(originalLocalScale, 0.2f));           // 2D representation of peg going into board
        }
    }

}