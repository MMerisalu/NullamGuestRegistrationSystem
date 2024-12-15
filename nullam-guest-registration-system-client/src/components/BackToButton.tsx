interface IBackToButton {
    to: string,
    children : string,
    //onClick: (to: string) => void
}

const BackToButton = ({to, children}: IBackToButton) => {
    return (
      <button className="btn btn-primary" onClick={() => window.location.href=to}>{children}</button>
    )
  }
  
  export default BackToButton;