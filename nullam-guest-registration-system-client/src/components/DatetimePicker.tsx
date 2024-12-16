import React, { useRef, useEffect } from 'react';
import flatpickr from 'flatpickr';
import 'flatpickr/dist/flatpickr.min.css';
import { Estonian } from 'flatpickr/dist/l10n/et.js';

export default function DatetimePicker() {
  const inputRef = useRef(null); // Initialize with null for DOM reference

  useEffect(() => {
    if (inputRef.current) {
      flatpickr(inputRef.current, {
        enableTime: true,
        dateFormat: 'd.m.Y H:i',  // Format according to Estonian standards
        locale: Estonian,  // Apply Estonian locale
      });
    }
  }, []);

  return <input ref={inputRef} className="form-control" placeholder='test' />;
}
