// objectUtils.js ou objectHelpers.js

function _get(obj, path) {
    if (obj == null) return undefined;
    if (path == null) return undefined;
    
    const parts = path.split('.');
    let result = obj;
    
    for (const part of parts) {
      if (result == null) return undefined;
      result = result[part];
    }
    
    return result;
  }
  
  function _set(obj, path, value) {
    if (typeof obj !== 'object' || obj === null) {
      obj = {};
    }
    
    const parts = path.split('.');
    const original = obj;
    
    for (let i = 0; i < parts.length - 1; i++) {
      const part = parts[i];
      
      if (typeof obj[part] !== 'object' || obj[part] === null) {
        const nextPart = parts[i + 1];
        obj[part] = !isNaN(nextPart) ? [] : {};
      }
      
      obj = obj[part];
    }
    
    const lastPart = parts[parts.length - 1];
    obj[lastPart] = value;
    
    return original;
  }
  
  // Exportação nomeada - permite importar funções específicas
  export { _get, _set };
  
  // Exportação padrão - exporta um objeto com ambas as funções
  export default {
    get: _get,
    set: _set
  };