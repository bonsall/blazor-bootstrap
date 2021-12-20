const propertyPrefix = '__bonzai_bootstrap_blazor__'

function getScrollHeight(element) {
   return element.scrollHeight;
}

function getScrollWidth(element) {
   return element.scrollWidth;
}

function addEventListener(element, event, objectReference, methodToCall) {
   if (element === null) return;

   const listener = function (evt) {
      objectReference.invokeMethodAsync(methodToCall, evt);
   }

   removeEventListener(element, event, methodToCall);
   element.addEventListener(event, listener)
   element[methodIdentifier(event, methodToCall)] = listener;
}

function removeEventListener(element, event, methodToCall) {
   if (element === null) return;

   element.removeEventListener(event, element[methodIdentifier(event, methodToCall)]);
   delete element[methodIdentifier(event, methodToCall)];
}

function methodIdentifier(event, methodToCall) {
   return propertyPrefix + '_' + event + '_' + methodToCall;
}

/**
   * Trick to restart an element's animation
   *
   * @param {HTMLElement} element
   * @return void
   *
   * @see https://www.charistheo.io/blog/2021/02/restart-a-css-animation-with-javascript/#restarting-a-css-animation
   */
function reflow(element) {
   element.offsetHeight;
}

function getBoundingClientRect(element) {
   return element.getBoundingClientRect();
}

export { getScrollHeight, addEventListener, removeEventListener, getBoundingClientRect, reflow, getScrollWidth }