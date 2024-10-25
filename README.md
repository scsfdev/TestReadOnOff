The demo showing how to send START (READON) and COMPLETION (READOFF) commands from Serial Port to QB33-SU.

For QB33-SU, remember to use "QB Setting Software" from DENSO WAVE to change below settings.
  1. BASIC SETTING >> OPERATION >> Make sure "Scan Mode > Mode" is set to "Continuous reading mode B" or "Auto sense mode".
  2. BASIC SETTING >> OPERATION >> Make sure "Scanning Start/Completion command" checkbox is selected.
  3. BASIC SETTING >> OPERATION >> Make sure "Scanning Start Command" is set to "READON".
  4. BASIC SETTING >> OPERATION >> Make sure "Scanning Completion Command" is set to "READOFF".

If you want to prevent double scan on same code more than one time, remember to set "BASIC SETTING >> OPERATION >> Double-read prevention time" to higher value.
